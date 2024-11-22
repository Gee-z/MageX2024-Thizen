using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
   public class ItemData
   {
        public string name;
        [TextArea(2, 5)]
        public string Description;
        public Sprite Image;
   }
    public ItemData[] ItemList;
    public List<Image> InventorySlots = new List<Image>();
    public List<int> ItemsOwned = new List<int>();
    public TMP_Text name;
    public TMP_Text Description;
    public Image image;
    public static InventoryManager instance;

    public GameObject InventoryContainer;
    public ScrollRect scrollRect;  
    public GameObject RightMenu;
    public bool Start = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
   public void OpenInventory()
   {
        InventoryContainer.SetActive(true);
        scrollRect.content = InventoryContainer.GetComponent<RectTransform>();
        for(int i =0;i<4;i++)
        {
            Color tempColor = InventorySlots[i].color;
            tempColor.a = 0f;
            InventorySlots[i].color = tempColor;
            InventorySlots[i].sprite = null;
        }
        for(int i=0;i<ItemsOwned.Count;i++)
        {
            Color tempColor = InventorySlots[i].color;
            tempColor.a = 1f;
            InventorySlots[i].color = tempColor;
            InventorySlots[i].sprite = ItemList[ItemsOwned[i]].Image;
        }
        if(ItemsOwned.Count !=0 ) ShowItem(0);
   }
   
   public void CloseInventory()
   {
        InventoryContainer.SetActive(false);
        RightMenu.SetActive(false);
   }
   public void ShowItem(int index)
   {
        Debug.Log("Showing Item");
        if(index < ItemsOwned.Count && ItemsOwned.Count != 0)
        {
            RightMenu.SetActive(true);
            name.text = ItemList[ItemsOwned[index]].name;
            Description.text = ItemList[ItemsOwned[index]].Description;
            image.sprite = ItemList[ItemsOwned[index]].Image;
        }
   }
   public void UseItem(int Items)
   {
        int index = ItemsOwned.IndexOf(Items);
        if(index != -1)
        {
            ItemsOwned.RemoveAt(index);
        }

   }
   public bool CheckItem(int Items)
   {
        int index = ItemsOwned.IndexOf(Items);
        if(index != -1)
        {
            return true;
        }
        else
        {
            return false;
        }
   }
   public void AddItem(int Index)
   {
        ItemsOwned.Add(Index);
   }

}
