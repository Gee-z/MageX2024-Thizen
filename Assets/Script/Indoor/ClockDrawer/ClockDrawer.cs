using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDrawer : MonoBehaviour
{
    public LogoButton[] Logo;
    public int[] Answer;
    public bool Completed;
    public Sprite Closed;
    public Sprite Locked;
    public Sprite Unlocked;
    public  SpriteRenderer Renderer;
    public GameObject Items;
   

    void Start()
    {
        Items.SetActive(false);
    }
    void Update()
    {
        
    }
    public void Open()
    {
        Renderer.gameObject.SetActive(true);
        if(!PuzzleManager.instance.clockPuzzle.Completed)
        {
            Renderer.sprite = Closed;
            DialogueManager.instance.StartDialog(4);
        }

        else if (PuzzleManager.instance.clockPuzzle.Completed && !Completed)
        {
            DialogueManager.instance.StartDialog(5);
            Renderer.sprite = Locked;
            Logo[0].gameObject.SetActive(true);
            Logo[1].gameObject.SetActive(true);
            Logo[2].gameObject.SetActive(true);
            Logo[3].gameObject.SetActive(true);
            Logo[4].gameObject.SetActive(true);
        }
        else if(Completed)
        {
            Renderer.sprite = Unlocked;
            Logo[0].gameObject.SetActive(false);
            Logo[1].gameObject.SetActive(false);
            Logo[2].gameObject.SetActive(false);
            Logo[3].gameObject.SetActive(false);
            Logo[4].gameObject.SetActive(false);
            Items.SetActive(true);
        }
    }
    public void Close()
    {
        Renderer.gameObject.SetActive(false);
    }
    public void CheckCompletion()
    {
        if(Logo[0].CurrentIndex == Answer[0] && Logo[1].CurrentIndex == Answer[1] && Logo[2].CurrentIndex == Answer[2] && Logo[3].CurrentIndex == Answer[3] && Logo[4].CurrentIndex == Answer[4])
        {
            DialogueManager.instance.StartDialog(6);
            Debug.Log("Completed");
            Completed = true;
            Open();
        }
    }
}
