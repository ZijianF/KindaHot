using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float BulletTimeFactor;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Target;
    [SerializeField] private float RotationSpeed = 0.5f;

    [SerializeField] private float RepositionRadius;

    [SerializeField] private GameObject Head;
    private bool InLineOfSight = false;
    

    // Should leave space for weapon prefab;

    // Start is called before the first frame update
    void Start()
    {
        // Head = GameObject.FindGameObjectWithTag("head");
        // Target = GameObject.Find("Player/The Adventurer Blake/armature/Bone/hips/Spine/Spine1/chest/neck/head");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("update");
        // Debug.DrawRay(Head.gameObject.transform.position, transform.TransformDirection(Vector3.forward) * 10000.0f, Color.blue);
        RaycastHit hit;
        var rayDirection = Target.transform.position - Head.transform.TransformPoint(Vector3.zero);
        Debug.DrawRay(Head.transform.position, rayDirection * 10.0f, Color.blue);
        Debug.Log("Player postion " + Target.transform.position.x+ " " + Target.transform.position.y + " " + Target.transform.position.z);
        // Debug.Log("Head postion " + Head.transform.position.x+ " " + Head.transform.position.y + " " + Head.transform.position.z);
        if (Physics.Raycast(Head.transform.position, rayDirection, out hit)) 
        {
            Debug.Log("postion " + hit.point.x+ " " + hit.point.y + " " + hit.point.z);
            bool hitPlayer = false;
            float hitDistance = Mathf.Sqrt(
                Mathf.Pow((Target.transform.position.x - hit.point.x), 2.0f) +
                Mathf.Pow((Target.transform.position.y - hit.point.y), 2.0f) +
                Mathf.Pow((Target.transform.position.z - hit.point.z), 2.0f)
            );
            Debug.Log("Distance " + hitDistance);
            if (hitDistance < 0.4)
            {
                // Saw the target
                Debug.DrawRay(Head.gameObject.transform.position, rayDirection * 10000f, Color.yellow);
                // Rotate toward player when see 
                Vector3 direction = rayDirection.normalized;
                Quaternion lookRotation = Quaternion.LookRotation(rayDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            } else
            {
                // Did not find target
                Debug.DrawRay(Head.gameObject.transform.position, rayDirection * 10000f, Color.red);
            }
        }
    }
}
