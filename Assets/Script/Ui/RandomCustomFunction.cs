using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomCustomFunction : MonoBehaviour
{
    bool AlreadyPass = false;
    public GameObject Alex;
    public GameObject Alex2;
    public bool HasInteracted = false;
    public void UseDrill()
    {
        if(InventoryManager.instance.CheckItem(4))
        {
            HasInteracted = true;
            FadeTransition.instance.Fade();
            Invoke("Delayed",1f);
        }
        else if(!HasInteracted)
        {
            DialogueManager.instance.StartDialog(2);
        }
        else if(HasInteracted)
        {
            DialogueManager.instance.StartDialog(8);
        }
    }
    public void Delayed()
    {
        Alex.SetActive(true);
        Alex2.SetActive(false);
        InventoryManager.instance.UseItem(4);
        DialogueManager.instance.StartDialog(8);
    }
    public void DialogDome()
    {
        if(!AlreadyPass)
        {
            AlreadyPass = true;
            DialogueManager.instance.StartDialog(4);
        }
    }
    public void Teleport()
    {
        DialogueManager.instance.StartDialog(2);
    }
    public void GoToPlanet()
    {
        AudioManager.instance.ChangeMusic("Hub","Overworld");
        FadeTransition.instance.ChangeScene(1);
    }
    public void EnterHub()
    {
        if(!HasInteracted)
        {
            MenuManager.instance.NewRotation = true; 
            MenuManager.instance.SavedRotation = MovementManager.instance.PlanetRb.rotation;
            AudioManager.instance.ChangeMusic("Overworld","Hub");
            FadeTransition.instance.ChangeScene(3);
        }
        else
        {
            AudioManager.instance.ChangeMusic("Overworld","MainMenu");
            FadeTransition.instance.Fade();
            Invoke("AnotherDelay",1f);
            
        }
    }
    public void AnotherDelay()
    {
        DialogueManager.instance.StartDialog(9);
        DialogueManager.instance.FinalDialogue=true;
        DialogueManager.instance.LeftRenderer.gameObject.SetActive(false);
        DialogueManager.instance.RightRenderer.gameObject.SetActive(false);
        DialogueManager.instance.Square.GetComponent<SpriteRenderer>().color = Color.black;
    }
    public GameObject Tutorial;
    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
    }

}
