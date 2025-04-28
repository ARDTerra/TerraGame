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

        FindAnyObjectByType<ItemUiSlot>().CurrentItemsFound++;

        this.transform.position =  new Vector3(transform.position.x, transform.position.y - 5, transform.position.z); // sends object below map to hide
        StartCoroutine(WaitForDeath(1f)); // calls coroutine and waits for x amount of seconds 
    }

    private IEnumerator WaitForDeath(float waitTime) // wait for death
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }
}
