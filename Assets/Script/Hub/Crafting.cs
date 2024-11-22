using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public CraftingPiece[] Pieces;
    public float Offset;
    public bool Completed;
    public GameObject BluePrint;
    public void OpenCrafting()
    {
        if(InventoryManager.instance != null)
        {
            if((InventoryManager.instance.CheckItem(1)&&InventoryManager.instance.CheckItem(2)&&InventoryManager.instance.CheckItem(3)))
            {
                BluePrint.SetActive(true);
            }
            else
            {
                DialogueManager.instance.StartDialog(1);
            }
        }
        else 
                DialogueManager.instance.StartDialog(1);
    }
    public void CheckCompletion()
    {
        if(Vector2.Distance(Pieces[0].TargetPosition, Pieces[0].gameObject.transform.position) < Offset &&Vector2.Distance(Pieces[1].TargetPosition, Pieces[1].gameObject.transform.position)<Offset &&Vector2.Distance(Pieces[2].TargetPosition, Pieces[2].gameObject.transform.position)<Offset )
        {
            if(InventoryManager.instance != null)
            {
                InventoryManager.instance.UseItem(1);
                InventoryManager.instance.UseItem(2);
                InventoryManager.instance.UseItem(3);
                InventoryManager.instance.AddItem(4);
                BluePrint.SetActive(false);
                DialogueManager.instance.StartDialog(3);
            }
        }
    }   
}
