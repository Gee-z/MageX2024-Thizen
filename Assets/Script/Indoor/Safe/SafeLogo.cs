using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeLogo : MonoBehaviour
{
    public int CurrentIndex;
    public Sprite[] logo;
    public Safe Safe;
    public void More()
    {
        CurrentIndex++;
        if(CurrentIndex >= 6) CurrentIndex = 0;
        GetComponent<SpriteRenderer>().sprite = logo[CurrentIndex];
        Safe.CheckCompletion();
    }
    public void Less()
    {
        CurrentIndex--;
        if(CurrentIndex < 0) CurrentIndex = 5;
        GetComponent<SpriteRenderer>().sprite = logo[CurrentIndex];
        Safe.CheckCompletion();
    }
}
