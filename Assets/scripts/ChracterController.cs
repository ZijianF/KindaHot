using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Command;

public class ChracterController : MonoBehaviour
{
    [SerializeField] private bool BulletTime = false;
    [SerializeField] private GameObject Weapon;

    [SerializeField] private float MaxSpeed = 3f;
    // The time take the character accelerate from still to max speed.
    [SerializeField] private float TimeZeroToMax = 0.5f;

    

    [SerializeField] private float SpeedForward;
    [SerializeField] private float SpeedBackward;
    [SerializeField] private float SpeedLeft;
    [SerializeField] private float SpeedRight;
    
    float AccelRatePerSec;
    float Speed;

    // private ICharacterMoveCommand Fire1;
    private ICharacterMoveCommand Forward;
    private ICharacterMoveCommand Backward;
    private ICharacterMoveCommand Left;
    private ICharacterMoveCommand Right;
    private ICharacterMoveCommand Roll;
    private Vector3 MovementDirection; 
    private Rigidbody Rb;
    

    
    // Start is called before the first frame update
    void Start()
    {
        this.Forward = this.gameObject.GetComponent<MoveCharacterForward>();
        Speed = this.MaxSpeed;
        this.Rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.MovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        this.gameObject.transform.Translate(this.MovementDirection * Speed);
        // Rb.velocity = Transform.Translate(this.MovementDirection * Speed);
        
    }
}
