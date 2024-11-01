using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLockTrigger : MonoBehaviour
{
    private bool Triggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Entered");
        if (other.CompareTag("Drone")) 
        {
            Triggered = true;
            TriggerDetected();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player Exit");
        if (other.CompareTag("Drone")) 
        {
            Triggered = false;
            TriggerDetected();
        }
    }
    void TriggerDetected()
    {
        if(Triggered) MovementManager.instance.LockRotation();
        
        else MovementManager.instance. UnlockRotation();
    }
}
