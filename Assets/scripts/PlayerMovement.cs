using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool BulletTime = false;

    [SerializeField] private float MaxSpeed = 0.005f;
    // The time take the character accelerate from still to max speed.
    [SerializeField] private float TimeZeroToMax = 0.5f;

    [SerializeField] private float TurnSpeed = 5f;

    [SerializeField] private CharacterController Controller;

    [SerializeField] private float Sensitivity = 1.0f;


    float AccelRatePerSec;
    float Speed;

    // Target object for the camera to follow
    public GameObject FollowTarget;

    public Quaternion NextRotation;

    public float rotationLerp = 0.5f;

    public float TurnSmoothTime = 0.1f;
    public float TurnSmoothSpeed;

    public Vector2 _move;

    private Vector3 FacingDirection;
    private Vector3 MovementDirection;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        this.Controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        this.MovementDirection = new Vector3(horizontal, 0.0f, vertical);

        // Part 3

        // Setting variables for animator
        animator.SetFloat("Speed", vertical);

        // transform.Rotate(Vector3.up, horizontal * this.TurnSpeed * Time.deltaTime);

        var horizontalMouseMovement = Input.GetAxis("Mouse X");
        var cameraRotation = Quaternion.AngleAxis(horizontalMouseMovement  * Sensitivity * Time.deltaTime, Vector3.up);


        FollowTarget.transform.rotation *= cameraRotation;

        // Vertical rotation
        FollowTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime, Vector3.right);

        var angles = FollowTarget.transform.localEulerAngles;
        angles.z = 0;
        var angle = FollowTarget.transform.localEulerAngles.x;

        // Clamp the Up/Down rotation
        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        FollowTarget.transform.localEulerAngles = angles;


        // Problematic
        // if(horizontalMouseMovement != 0) 
        // {
        //     transform.rotation *= cameraRotation;
        //     transform.localEulerAngles = angles;
        // }


        if (vertical == 0 && horizontal ==0)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("IsMovingForward", false);
            animator.SetBool("IsMovingBackward", false);
        }

        if (vertical != 0) 
        {
            // var rotationVector = FollowTarget.transform.rotation.eulerAngles;
            // rotationVector.x = 0;
            // rotationVector.z = 0;
            // transform.rotation = Quaternion.Euler(rotationVector);\
            animator.SetBool("Idle", false);
            if(vertical > 0) 
            {
                animator.SetBool("IsMovingForward", true);
                transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
                FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0 ,0);
                Controller.SimpleMove(transform.forward * MaxSpeed * vertical);
            }

            if(vertical < 0)
            {
                animator.SetBool("IsMovingBackward", true);
                transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
                FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0 ,0);
                Controller.SimpleMove(transform.forward * MaxSpeed * vertical);
            }
        }

        if (horizontal != 0)
        {
            animator.SetBool("Idle", false);
            transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
            FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0 ,0);
            Controller.SimpleMove(transform.right * MaxSpeed * horizontal);

        }

        // if(vertical != 0 || horizontal != 0) 
        // {
        //     float targetAngle = Mathf.Atan2(this.MovementDirection.x, this.MovementDirection.z) * Mathf.Rad2Deg;
        //     float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothSpeed, TurnSmoothTime);
        //     transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        // }









        
        // if(MovementDirection.magnitude >= 0.1f) 
        // {
        //     float targetAngle = Mathf.Atan2(this.MovementDirection.x, this.MovementDirection.z) * Mathf.Rad2Deg;
        //     float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothSpeed, TurnSmoothTime);
        //     transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        //     this.Controller.Move(this.MovementDirection * Time.deltaTime * MaxSpeed);
        // }
        
        // // Rotate the follow target based on input.
        // FollowTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X")  * Sensitivity * Time.deltaTime, Vector3.up);

        // // Vertical rotation
        // FollowTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime, Vector3.right);

        // var angles = FollowTarget.transform.localEulerAngles;
        // angles.z = 0;
        // var angle = FollowTarget.transform.localEulerAngles.x;

        // // Clamp the Up/Down rotation
        // if (angle > 180 && angle < 300)
        // {
        //     angles.x = 300;
        // }
        // else if(angle < 180 && angle > 40)
        // {
        //     angles.x = 40;
        // }

        // FollowTarget.transform.localEulerAngles = angles;

        // NextRotation = Quaternion.Lerp(FollowTarget.transform.rotation, NextRotation, Time.deltaTime * rotationLerp);

        // animator.SetFloat("Speed", MovementDirection.magnitude);

        // if(_move.x == 0 && _move.y == 0)
        // {
        //     NextPosition = transform.position;
        //     return;
        // }

        // Vector3 position = (transform.forward * _move.y * MaxSpeed) + (transform.right * _move.x * MaxSpeed);
        // NextPosition = transform.position + position;

        // transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);

        // FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        // NextRotation = Quaternion.Lerp(FollowTarget.transform.rotation, NextRotation, Time.deltaTime * rotationLerp);


    }
}
