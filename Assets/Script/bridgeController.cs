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

    void Start()
    {
        // Debug.Log(bridge.transform.localEulerAngles.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Entered");
        if (other.CompareTag("Player") || other.CompareTag("Drone")) 
        {
            Triggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player Exited");
        if (other.CompareTag("Player") || other.CompareTag("Drone")) 
        {
            Triggered = false;
        }
    }
    void Update()
    {
        if(Triggered && Input.GetKeyDown(KeyCode.F))
        {
            // Debug.Log("interacting");
            interactBridge();
        }
    }
    void interactBridge()
    {
        // bridge.transform.localRotation = Quaternion.Euler(0, 0, closedAngle);
        bridge.transform.DORotate(new Vector3(0, 0, closedAngle - bridge.transform.localEulerAngles.z), 1f, RotateMode.LocalAxisAdd )
        .SetEase(Ease.InOutQuad)
        .OnComplete(() => 
        {
            Barrier.enabled = false;
            // Debug.Log("Bridge Opened");
        });
        
    }
    
}
