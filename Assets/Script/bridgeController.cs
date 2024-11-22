using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class bridgeController : MonoBehaviour
{
    public GameObject bridge;
    public PolygonCollider2D Barrier;
    public float closedAngle;
    private bool Triggered = false;
    public bool isRight=false;
    private bool isOpened = false;
    public bool isCompleted = false;
    public GameObject Popup;
    public GameObject PopupTutorial;


    void Start()
    {
        if((InventoryManager.instance.CheckItem(1)&&InventoryManager.instance.CheckItem(2)&&InventoryManager.instance.CheckItem(3)) ||InventoryManager.instance.CheckItem(4))
        {
            isCompleted=true;
            Barrier.enabled = false;
            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.z = closedAngle;
            bridge.transform.localEulerAngles = currentRotation;
        }
        Popup.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Entered");
        if (other.CompareTag("Player") || other.CompareTag("Drone")) 
        {
            if(!isCompleted)
            {
                Popup.SetActive(true);
                Triggered = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player Exited");
        if (other.CompareTag("Player") || other.CompareTag("Drone")) 
        {
            Triggered = false;
            Popup.SetActive(false);
        }
    }
    public void TurnoffTutorial()
    {
        PopupTutorial.SetActive(false);
    }
    void Update()
    {
        if(Triggered && Input.GetKeyDown(KeyCode.F) && !isOpened)
        {
            isOpened = true;
            
            Popup.SetActive(false);
            // Debug.Log("interacting");
            if(!isRight)
            {
                interactBridge();
                DialogueManager.instance.StartDialog(6);
                PopupTutorial.SetActive(true);
            }
            else
            {
                FlowManager.instance.SpawnLevel1();
            }
        }
    }
    public void interactBridge()
    {
        // bridge.transform.localRotation = Quaternion.Euler(0, 0, closedAngle);
        Debug.Log(closedAngle - bridge.transform.localEulerAngles.z);
        bridge.transform.DORotate(new Vector3(0, 0, closedAngle - bridge.transform.localEulerAngles.z), 1f, RotateMode.LocalAxisAdd )
        .OnComplete(() => 
        {
            Barrier.enabled = false;
            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.z = closedAngle; // Set the Z rotation
            bridge.transform.localEulerAngles = currentRotation;
            isCompleted = true;
        });
        
    }
    
}
