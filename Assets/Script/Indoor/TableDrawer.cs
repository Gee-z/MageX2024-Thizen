using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDrawer : MonoBehaviour
{
    public SpriteRenderer Renderer;
    public Sprite Unlocked;
    public GameObject item;
    public bool Completed;
    public BoxCollider2D Collider;
    void Start()
    {
        
    }
    void OnMouseDown()
    {
        if(InventoryManager.instance.CheckItem(0))
        {
            InventoryManager.instance.UseItem(0);
            DialogueManager.instance.StartDialog(18);
            Debug.Log("Clicked");
            Renderer.sprite = Unlocked;
            item.SetActive(true);
            Completed = true;
            Collider.enabled = false;
        }
    }
    public void Open()
    {
        Collider.enabled = true;
        if(!Completed)DialogueManager.instance.StartDialog(17);
        Renderer.gameObject.SetActive(true);
    }
    public void Close()
    {
        Collider.enabled = false;
        Renderer.gameObject.SetActive(false);
    }
}
