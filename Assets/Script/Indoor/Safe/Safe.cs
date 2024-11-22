using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public SafeLogo[] Logo;
    public bool Completed;
    public int[] Answer;

    public Sprite Locked;
    public Sprite Unlocked;
    public SpriteRenderer Renderer;
    public GameObject Items;

    public void CheckCompletion()
    {
        if(Logo[0].CurrentIndex == Answer[0] && Logo[1].CurrentIndex == Answer[1] && Logo[2].CurrentIndex == Answer[2] && Logo[3].CurrentIndex == Answer[3] )
        {
            Debug.Log("Completed");
            Completed = true;
            Renderer.sprite = Unlocked;
            Items.SetActive(true);
            Logo[0].gameObject.SetActive(false);
            Logo[1].gameObject.SetActive(false);
            Logo[2].gameObject.SetActive(false);
            Logo[3].gameObject.SetActive(false);
            DialogueManager.instance.StartDialog(10);
        }
    }
    public void Open()
    {
        Renderer.gameObject.SetActive(true);
        if(!Completed) DialogueManager.instance.StartDialog(9);
    }
    public void Close()
    {
        Renderer.gameObject.SetActive(false);
    }
}
