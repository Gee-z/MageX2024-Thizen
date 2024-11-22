using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Baterry : MonoBehaviour
{
    public bool isTrigered;


    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if(other.CompareTag("Drone"))
        {
            isTrigered = true;
            InventoryManager.instance.AddItem(5);
            this.gameObject.SetActive(false);
            DialogueManager.instance.StartDialog(5);
        }
    }
}
