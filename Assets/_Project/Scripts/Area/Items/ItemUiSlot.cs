using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemUiSlot : MonoBehaviour
{
    [Header("Item Settings")]
    public List<ItemData> allItems; // all items list

    [Header("UI Settings")]
    public GameObject uiSlotPrefab; // Ui prefab
    public Transform uiParent; // Ui Background parent

    [Header("World Spawn Settings")]
    public Transform spawnPointParent; // parent object with all spawn spaces
    public Transform spawnParent; // main parent to clean scene

    private List<Transform> spawnPoints = new(); // spawn point list
    private List<ItemInstance> spawnedItems = new(); // List of items

    [Header("Amount")]
    public int MaxAmountItemsFound; // item amount to be spawned
    public int CurrentItemsFound; // current amount

    void Start()
    {
        // sets all spawn locations from parents and adds them to list
        foreach (Transform child in spawnPointParent)
        {
            spawnPoints.Add(child);
        }

        // sees if any spawnpoint areas are ocupied or taken
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        // repeats for each item
        for (int i = 0; i < MaxAmountItemsFound; i++)
        {
            if (availableSpawnPoints.Count == 0) //Just makes sure the game dosnt bug out if not enough spawn locations
            {
                Debug.LogWarning("Not enough unique spawn points for all items!");
                break;
            }

            // picks item from item data
            ItemData randomItem = allItems[Random.Range(0, allItems.Count)];

            // creates a ui slot in background and sets the colour to gray
            GameObject uiSlot = Instantiate(uiSlotPrefab, uiParent);
            Image iconImage = uiSlot.GetComponentInChildren<Image>();
            iconImage.sprite = randomItem.icon;
            iconImage.color = Color.gray;

            // picks a spawnpoint that is avaliable for item
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform chosenPoint = availableSpawnPoints[randomIndex];
            availableSpawnPoints.RemoveAt(randomIndex);

            // spawns the object within the world at the spawn point
            Vector3 spawnPos = chosenPoint.position;
            GameObject worldObj = Instantiate(randomItem.worldPrefab, spawnPos, Quaternion.identity, spawnParent);

            // adds a number to object to make refrence to UI
            string uniqueID = System.Guid.NewGuid().ToString();
            ItemScript pickup = worldObj.GetComponent<ItemScript>();
            if (pickup != null)
            {
                pickup.itemID = uniqueID;
            }

            // links item to UI
            spawnedItems.Add(new ItemInstance { id = uniqueID, uiIcon = iconImage});
        }
    }

    // this is called when item is collected. With reference to the id number
    public void CollectItem(string id)
    {
        foreach (var item in spawnedItems) // does this for each item
        {
            if (item.id == id) // checks if the id is correct
            {
                item.uiIcon.color = Color.green; // sets the ui image to green
                return;
            }
        }
    }
}

// item instace and data for the items id and the Ui that is is linked to.
[System.Serializable]
public class ItemInstance
{
    public string id;
    public Image uiIcon;
}
