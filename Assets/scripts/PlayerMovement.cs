using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool BulletTime = false;

    [SerializeField] private bool Aiming = false;

    [SerializeField] private float MaxSpeed = 0.005f;
    // The time take the character accelerate from still to max speed.
    [SerializeField] private float TimeZeroToMax = 0.5f;

    [SerializeField] private float TurnSpeed = 5f;

    [SerializeField] private CharacterController Controller;

    [SerializeField] private float Sensitivity = 1.0f;

    [SerializeField] private float BulletTimeFactor = 0.5f;

    [SerializeField] private int Health = 5;

    [SerializeField] private bool Dead = false;

    [SerializeField] private GameObject Crosshair;

    [SerializeField] GameObject Gun1;

    public GameObject MovingCamera;
    public GameObject AimCamera;

    [SerializeField] GameObject Cube;



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

    private GunController GunController;

    // ************************************
    [SerializeField]
    private GameObject ScriptHome;

    private PublisherManager PublisherManager;
    private BulletTimeWatcher Watcher;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        this.Controller = GetComponent<CharacterController>();

        // ****************************************
        // this.PublisherManager = GameObject.FindGameObjectWithTag("ScriptHome").GetComponent<PublisherManager>();
        this.PublisherManager = ScriptHome.GetComponent<PublisherManager>();
        this.PublisherManager.Subscribe(TriggeringBulletTime);
        GunController = Gun1.GetComponent<GunController>();

        // Watcher = new BulletTimeWatcher()
    }

    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GunController.Shoot();
        }


        if (this.Dead)
        {
            animator.SetBool("Dead", false);
            return;
        }

        this.Aiming = false;
        if (Input.GetButton("Fire2"))
        {
            this.Aiming = true;
        }

        if (this.Aiming)
        {
            this.MovingCamera.SetActive(false);
            this.AimCamera.SetActive(true);
            this.Crosshair.SetActive(true);
        }
        else if (!this.Aiming)
        {
            this.MovingCamera.SetActive(true);
            this.AimCamera.SetActive(false);
            this.Crosshair.SetActive(false);
        }

        var sensitivity = this.Sensitivity;
        var speed = this.MaxSpeed;
        //  ****************************************
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (this.BulletTime)
            {
                PublisherManager.BulletTimeInitiator(false);
            }
            else
            {
                PublisherManager.BulletTimeInitiator(true);
            }
            Debug.Log("triggered bullet time " + this.BulletTime);
        }
        //GunController.RenewDirection(new Vector3(0, 0, 0));
        if (Aiming)
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.blue);
            RaycastHit hit;
            Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                Debug.Log("hit.point and collider position" + hit.point + " " + hit.collider.gameObject.transform.position);
                //Instantiate(Cube, hit.point, Quaternion.identity);
                GunController.RenewDirection(hit.point);//- Camera.main.transform.position);
            }

        }

        //if (Input.GetButtonDown("Fire2"))
        //{
        //    this.Aiming = true;

        //}


        //if (Aiming)
        //{
        //    this.AimCamera.SetActive(true);
        //    this.MovingCamera.SetActive(false);
        //}
        //else
        //{
        //    this.AimCamera.SetActive(false);
        //    this.MovingCamera.SetActive(true);
        //}

        if (this.BulletTime)
        {
            // sensitivity *= BulletTimeFactor;
            // speed *= BulletTimeFactor;
            SlowTimeDown();
        }
        else
        {
            BackToNoamalTime();
        }

        //  ****************************************

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        this.MovementDirection = new Vector3(horizontal, 0.0f, vertical);


        // Setting variables for animator
        animator.SetFloat("Speed", vertical);

        // transform.Rotate(Vector3.up, horizontal * this.TurnSpeed * Time.deltaTime);

        var horizontalMouseMovement = Input.GetAxis("Mouse X");
        var cameraRotation = Quaternion.AngleAxis(horizontalMouseMovement * sensitivity * Time.deltaTime, Vector3.up);


        FollowTarget.transform.rotation *= cameraRotation;

        // Vertical rotation
        FollowTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, Vector3.right);

        var angles = FollowTarget.transform.localEulerAngles;
        angles.z = 0;
        var angle = FollowTarget.transform.localEulerAngles.x;

        // Clamp the Up/Down rotation
        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        FollowTarget.transform.localEulerAngles = angles;


        if (vertical == 0 && horizontal == 0)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("IsMovingForward", false);
            animator.SetBool("IsMovingBackward", false);
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", false);
        }

        if (vertical == 0)
        {
            animator.SetBool("IsMovingForward", false);
            animator.SetBool("IsMovingBackward", false);
        }

        if (vertical != 0)
        {
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", false);
            animator.SetBool("Idle", false);
            if (vertical > 0)
            {
                animator.SetBool("IsMovingForward", true);
                transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
                FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                Controller.SimpleMove(transform.forward * speed * vertical);
            }

            if (vertical < 0)
            {
                animator.SetBool("IsMovingBackward", true);
                transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
                FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                Controller.SimpleMove(transform.forward * speed * vertical);
            }
        }

        if (horizontal != 0)
        {
            animator.SetBool("Idle", false);
            if (horizontal > 0)
            {
                animator.SetBool("IsMovingRight", true);
                transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
                FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                Controller.SimpleMove(transform.right * speed * horizontal);
            }

            if (horizontal < 0)
            {
                animator.SetBool("IsMovingLeft", true);
                transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
                FollowTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                Controller.SimpleMove(transform.right * speed * horizontal);
            }

        }
        // TakeDamage();

    }

    private void TriggeringBulletTime(bool isOn)
    {
        this.BulletTime = isOn;
    }

    private void SlowTimeDown()
    {
        Time.timeScale = BulletTimeFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    private void BackToNoamalTime()
    {
        Time.timeScale = 1;
    }

    public void TakeDamage()
    {
        this.Health -= 1;
        if (this.Health <= 0)
        {
            this.Dead = true;
            EnterDeathAnimation();
        }
    }

    private void EnterDeathAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("IsMovingForward", false);
        animator.SetBool("IsMovingBackward", false);
        animator.SetBool("IsMovingRight", false);
        animator.SetBool("IsMovingLeft", false);
        animator.SetBool("Dead", true);
    }

    public bool IsDead()
    {
        return this.Dead;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("hit " + hit.collider.gameObject.name);
        if (hit.collider.gameObject.name == "goal")
        {
            Debug.Log("it's HIM!");
            hit.transform.SendMessage("Touched", SendMessageOptions.DontRequireReceiver);
        }
    }
}
