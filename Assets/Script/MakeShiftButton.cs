using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MakeShiftButton : MonoBehaviour
{
    public UnityEvent myEvent;

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        Debug.Log("Clicked");
        myEvent.Invoke();
    }
}
