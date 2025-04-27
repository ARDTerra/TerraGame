using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // each item has its own id, this sets it
    public string itemID;

    // what happends when the player interacts with it
    public void Collect()
    {
        ItemUiSlot manager = FindFirstObjectByType<ItemUiSlot>(); // find this on the game manager
        manager.CollectItem(itemID); // sends a message to the script with the id of the item
        // NEED TO ADD A THING THAT INCREASE THE VVALUE OF ALL COLLECTED ITEMS. SO THE PLAYER CAN LEAVE ---------------------------
        Destroy(gameObject); // destorys it lmaooo
    }
}
