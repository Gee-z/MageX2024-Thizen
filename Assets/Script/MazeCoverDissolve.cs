using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MazeCoverDissolve : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color Show;
    public Color Hide;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Drone Entered");
        if (other.CompareTag("Drone")) 
        {
            HideCover();
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Drone Exited");
        if (other.CompareTag("Drone")) 
        {
            ShowCover();
        }
    }
    void ShowCover()
    {
        // spriteRenderer.color = Show;
        spriteRenderer.DOColor(Show, 1f);
    }
    void HideCover()
    {
        // spriteRenderer.color = Hide;
        spriteRenderer.DOColor(Hide, 1f);
    }
}
