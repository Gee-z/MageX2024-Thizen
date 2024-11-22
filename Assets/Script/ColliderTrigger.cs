using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    public bool isTrigered;
    public bool NotAButton;
    public UnityEvent myEvent;

    public GameObject Popup;
    void Start()
    {
        Popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isTrigered && !NotAButton)
        {
            if(PuzzleManager.instance != null)
            {
                if(!PuzzleManager.instance.OpeningUi)
                {
                    myEvent.Invoke();
                }
            }
            else
            {
                    myEvent.Invoke();
            }
        } 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isTrigered = true;
            if(NotAButton)
            {
                myEvent.Invoke();
            }
            else
            {
                Popup.SetActive(true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isTrigered = false;
            Popup.SetActive(false);
        }
    }
}
