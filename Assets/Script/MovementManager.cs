
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class MovementManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed = 100f;
    public float DroneSpeed = 100f;
    public GameObject Drone;
    public GameObject Player;
    public Rigidbody2D DroneRb;
    public Rigidbody2D PlanetRb;
    public Rigidbody2D PlayerRb;
    public Transform PlanetTrans;
    public Transform target;
    public Transform Object;
    public Transform DroneSlot;
    // public float rotationSpeed = 5f; // Speed of rotation
    private bool IsDroning;
    private float savedRotationPlayer;
    private float savedRotationDrone;
    private bool DroneRecalled;
    private bool Clickable;
    private bool DroneFacingRight;
    public bool lockPlanetRotation = false;
    static public MovementManager instance;
    public float rotationSpeedDroneRotate = 5f;
    public Animator anim;
    private bool isMoving;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        PlanetRb.centerOfMass = new Vector2(0, 0);
        DroneRecalled = true;
        Clickable = true;
        if (MenuManager.instance.NewRotation)
        {
            MenuManager.instance.NewRotation = false;
            PlanetRb.rotation = MenuManager.instance.SavedRotation;
        }
        Player.SetActive(true);
        Drone.SetActive(true);
    }
    private Vector2 movementInput;
    void Update()
    {
        if (!DialogueManager.instance.isDialogue && !FlowManager.instance.PlayingFlow)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Clickable)
                {

                    if (IsDroning)
                    {
                        StartCoroutine(ResetDroning());
                    }
                    else
                    {
                        StartCoroutine(StartDroning());
                    }
                    DroneRb.velocity = new Vector2(0f, 0f);
                    PlayerRb.velocity = new Vector2(0f, 0f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RecallDrone();
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            movementInput = new Vector2(horizontalInput, verticalInput);
            if (Clickable)
            {
                if (!IsDroning)
                {
                    if (PlanetRb.freezeRotation) PlanetRb.freezeRotation = false;
                    DroneRb.velocity = new Vector2(0f, 0f);
                    float angularVelocity = horizontalInput * rotationSpeed;
                    PlanetRb.angularVelocity = angularVelocity;
                    if (horizontalInput == 0)
                        anim.SetBool("Running", false);
                    else
                        anim.SetBool("Running", true);
                    // Debug.Log(horizontalInput);
                    if (horizontalInput > 0) Player.transform.localScale = new Vector3(Player.transform.localScale.x, 1f, Player.transform.localScale.z);
                    else if (horizontalInput < 0) Player.transform.localScale = new Vector3(Player.transform.localScale.x, -1f, Player.transform.localScale.z);
                    if (DroneRecalled)
                    {
                        float distance = Vector3.Distance(Drone.transform.position, DroneSlot.position);
                        if (distance > 0.2f)
                        {
                            // Debug.Log("To Far");
                            Drone.transform.position = Vector3.Lerp(Drone.transform.position, DroneSlot.position, Time.deltaTime * 0.5f);
                        }
                        if (horizontalInput > 0) Drone.transform.localScale = new Vector3(-1f, Drone.transform.localScale.y, Drone.transform.localScale.z);
                        else if (horizontalInput < 0) Drone.transform.localScale = new Vector3(1f, Drone.transform.localScale.y, Drone.transform.localScale.z);
                        DroneRb.rotation = PlayerRb.rotation + 90f; ;
                    }
                }
                else if (lockPlanetRotation)
                {
                    if (!PlanetRb.freezeRotation) PlanetRb.freezeRotation = true;
                    Debug.Log("droning while locked");
                    DroneRb.velocity = new Vector2(horizontalInput * DroneSpeed, verticalInput * DroneSpeed);
                    // FlipDrone(horizontalInput);
                }
                else
                {
                    if (PlanetRb.freezeRotation) PlanetRb.freezeRotation = false;
                    DroneRb.velocity = new Vector2(0f, verticalInput * DroneSpeed);
                    float angularVelocity = horizontalInput * rotationSpeed;
                    PlanetRb.angularVelocity = angularVelocity;
                    // FlipDrone(horizontalInput);
                }
            }
            if (IsDroning)
            {
                RotateBasedOnInput();
            }
        }
        else
        {
            DroneRb.velocity = new Vector2(0f, 0f);
            PlanetRb.angularVelocity = 0;
            PlanetRb.freezeRotation = true;
        }
    }
    void FlipDrone(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            DroneFacingRight = true;
            Drone.transform.localScale = new Vector3(-1f, Drone.transform.localScale.y, Drone.transform.localScale.z);
        }
        else if (horizontalInput < 0)
        {
            DroneFacingRight = false;
            Drone.transform.localScale = new Vector3(1f, Drone.transform.localScale.y, Drone.transform.localScale.z);
        }
    }
    private void RecallDrone()
    {
        if (!IsDroning)
        {
            DroneRecalled = true;
            Drone.transform.position = DroneSlot.position;
            DroneRb.isKinematic = false;
        }
    }
    IEnumerator ResetDroning()
    {
        // Debug.Log("Reset Droning");
        PlanetRb.angularVelocity = 0;

        Clickable = false;

        DroneRb.isKinematic = true;

        savedRotationDrone = PlanetRb.rotation;

        yield return new WaitForSeconds(0.1f);

        PlanetRb.rotation = savedRotationPlayer;
        virtualCamera.Follow = Player.transform;

        yield return new WaitForSeconds(0.5f);

        Clickable = true;


        Player.GetComponent<Collider2D>().enabled = true;
        PlayerRb.isKinematic = false;
        PlayerRb.freezeRotation = false;

        IsDroning = !IsDroning;

    }
    IEnumerator StartDroning()
    {

        // Debug.Log("Start Droning");

        DroneRb.rotation = 0;

        PlanetRb.angularVelocity = 0;

        Clickable = false;

        Player.GetComponent<Collider2D>().enabled = false;

        PlayerRb.freezeRotation = true;

        PlayerRb.isKinematic = true;

        savedRotationPlayer = PlanetRb.rotation;

        virtualCamera.Follow = Drone.transform;

        IsDroning = !IsDroning;
        if (DroneRecalled)
        {
            DroneRecalled = false;
        }
        else
        {
            PlanetRb.rotation = savedRotationDrone;
            yield return new WaitForSeconds(0.5f);
        }
        DroneRb.isKinematic = false;
        Clickable = true;


    }
    public void LockRotation()
    {
        lockPlanetRotation = true;
        PlanetRb.angularVelocity = 0;

        DroneRb.constraints = RigidbodyConstraints2D.None;
        DroneRb.freezeRotation = true;
    }
    public void UnlockRotation()
    {
        lockPlanetRotation = false;
        PlanetRb.angularVelocity = 0;

        DroneRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        DroneRb.freezeRotation = true;
    }
    void RotateBasedOnInput()
    {
        float directionSign = Mathf.Sign(Drone.transform.localScale.x);

        if (movementInput.x == 0 && movementInput.y == 0)
        {
            float angle = 0f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            Drone.transform.rotation = Quaternion.Slerp(Drone.transform.rotation, targetRotation, rotationSpeedDroneRotate * Time.deltaTime);
        }
        else
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;

            // if (directionSign < 0)
            // {
            // }
            if(Drone.transform.localScale.x > 0)
                angle += 180f;
    
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            Drone.transform.rotation = Quaternion.Slerp(Drone.transform.rotation, targetRotation, rotationSpeedDroneRotate * Time.deltaTime);
        }
    }


}
