using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneManage : MonoBehaviour
{

    public void EnterLift()
    {
        if(InventoryManager.instance.CheckItem(5))
        {
            InventoryManager.instance.UseItem(5);
            MenuManager.instance.SavedRotation = 19;
            MenuManager.instance.NewRotation = true;
            AudioManager.instance.ChangeMusic("Overworld","Underground");
            FadeTransition.instance.ChangeScene(2);
        }
        else
        {
            DialogueManager.instance.StartDialog(3);
        }
    }
}
