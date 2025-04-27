using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string itemID;

    public void Collect()
    {
        ItemUiSlot manager = FindFirstObjectByType<ItemUiSlot>();
        manager.CollectItem(itemID);
        Destroy(gameObject);
    }
}
