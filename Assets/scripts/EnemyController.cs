using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float BulletTimeFactor;

    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject Target;

    [SerializeField] private float RotationSpeed = 0.5f;

    [SerializeField] private float SpawnRadius = 3.0f;

    [SerializeField] private GameObject Head;

    [SerializeField] private GameObject Weapon;

    [SerializeField] private int Health = 1;

    [SerializeField] private bool Dead = false;

    [SerializeField] private bool ShowBot = false;

    private bool InLineOfSight = false;

    private Animator animator;
    

    // Should leave space for weapon prefab;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Vector3 spawnLocation = new Vector3(this.gameObject.transform.position.x + SpawnRadius * Random.Range(0f, SpawnRadius) * (Random.Range(0, 2) *2 -1), 0f , this.gameObject.transform.position.z + SpawnRadius * Random.Range(0f, SpawnRadius) * (Random.Range(0, 2) *2 -1));
        this.transform.position = spawnLocation;
        // Head = GameObject.FindGameObjectWithTag("head");
        // Target = GameObject.Find("Player/The Adventurer Blake/armature/Bone/hips/Spine/Spine1/chest/neck/head");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.Dead)
        {
            animator.SetBool("Dead", false);
            animator.SetBool("Exit", true);
            // Destroy(this.gameObject);
            return;
        }
        // Debug.Log("update");
        // Debug.DrawRay(Head.gameObject.transform.position, transform.TransformDirection(Vector3.forward) * 10000.0f, Color.blue);
        RaycastHit hit;
        // var rayDirection = Target.transform.position - Head.transform.TransformPoint(Vector3.zero);
        var rayDirection = Target.transform.position - Head.transform.position;
        Debug.DrawRay(Head.transform.position, rayDirection * 10.0f, Color.blue);
        // Debug.Log("Player postion " + Target.transform.position.x+ " " + Target.transform.position.y + " " + Target.transform.position.z);
        // Debug.Log("Head postion " + Head.transform.position.x+ " " + Head.transform.position.y + " " + Head.transform.position.z);
        if (Physics.Raycast(Head.transform.position, rayDirection, out hit)) 
        {
            // Debug.Log("postion " + hit.point.x+ " " + hit.point.y + " " + hit.point.z);
            bool hitPlayer = false;
            float hitDistance = Mathf.Sqrt(
                Mathf.Pow((Target.transform.position.x - hit.point.x), 2.0f) +
                Mathf.Pow((Target.transform.position.y - hit.point.y), 2.0f) +
                Mathf.Pow((Target.transform.position.z - hit.point.z), 2.0f)
            );
            // Debug.Log("Distance " + hitDistance);
            if (hitDistance < 0.4)
            {
                this.InLineOfSight = true;
                // Saw the target
                Debug.DrawRay(Head.gameObject.transform.position, rayDirection * 10000f, Color.yellow);
                // Debug.Log("Name " + hit.collider.gameObject.name);
                
            } else
            {
                this.InLineOfSight = false;
                // Did not find target
                Debug.DrawRay(Head.gameObject.transform.position, rayDirection * 10000f, Color.red);
            }
        }
        if(this.InLineOfSight) 
        {
            Vector3 direction = rayDirection.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(rayDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        }
        // TakeDamage();
        if(this.ShowBot)
        {
            return;
        }
    }
    
    public void TakeDamage()
    {
        this.Health -= 1;
        if(this.Health <= 0)
        {
            this.Dead = true;
            EnterDeathAnimation();
        }
    }
    private void EnterDeathAnimation()
    {
        if(ShowBot)
        {
            animator.SetBool("Dead", true);
        }
        else{
            // animator.SetBool("Idle", false);
            // animator.SetBool("IsMovingForward", false);
            // animator.SetBool("IsMovingBackward", false);
            // animator.SetBool("IsMovingRight", false);
            // animator.SetBool("IsMovingLeft", false);
            animator.SetBool("Dead", true);
        }
        animator.SetBool("Idle", false);
        animator.SetBool("IsMovingForward", false);
        animator.SetBool("IsMovingBackward", false);
        animator.SetBool("IsMovingRight", false);
        animator.SetBool("IsMovingLeft", false);
        animator.SetBool("Dead", true);
    }
        
}
