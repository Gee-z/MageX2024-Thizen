using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoButton : MonoBehaviour
{
    public int CurrentIndex = 0;
    public Sprite[] Logos;
    public Sprite[] Blur;
    public SpriteRenderer Renderer;
    public bool ChangingSprite = false;
    public ClockDrawer ClockDrawer;

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if(!ChangingSprite && !ClockDrawer.Completed)
        {
            ChangingSprite = true;
            CurrentIndex ++;
            if(CurrentIndex >=5) CurrentIndex = 0;
            StartCoroutine(ChangeSprite());
        }
    }

    IEnumerator ChangeSprite()
    {
        Renderer.sprite = Blur[0];
        yield return new WaitForSeconds(0.05f);
        Renderer.sprite = Blur[1];
        yield return new WaitForSeconds(0.05f);
        Renderer.sprite = Blur[0];
        yield return new WaitForSeconds(0.05f);
        Renderer.sprite = Logos[CurrentIndex];
        ChangingSprite = false;
        ClockDrawer.CheckCompletion();
    }
}
