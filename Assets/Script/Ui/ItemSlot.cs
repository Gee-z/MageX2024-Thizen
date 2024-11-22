using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public int Index;
    public void Click()
    {
        InventoryManager.instance.ShowItem(Index);
    }
}
