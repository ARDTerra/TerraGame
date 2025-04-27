using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemUiSlot : MonoBehaviour
{
    [Header("Item Settings")]
    public List<ItemData> allItems;

    [Header("UI Settings")]
    public GameObject uiSlotPrefab;
    public Transform uiParent;

    [Header("World Spawn Settings")]
    public Transform spawnPointParent; // parent containing all spawn point children
    public Transform spawnParent; // optional, to keep spawned items organized

    private List<Transform> spawnPoints = new();
    private List<ItemInstance> spawnedItems = new();

    [Header("Amount")]
    public int MaxAmountItemsFound;
    public int CurrentItemsFound;

    void Start()
    {
        // Get all child spawn points from the parent object
        foreach (Transform child in spawnPointParent)
        {
            spawnPoints.Add(child);
        }

        for (int i = 0; i < MaxAmountItemsFound; i++)
        {
            // Pick a random item (duplicates allowed)
            ItemData randomItem = allItems[Random.Range(0, allItems.Count)];

            // --- Create UI Slot ---
            GameObject uiSlot = Instantiate(uiSlotPrefab, uiParent);
            Image iconImage = uiSlot.GetComponentInChildren<Image>();
            iconImage.sprite = randomItem.icon;
            iconImage.color = Color.gray;

            // --- Spawn World Object ---
            Vector3 spawnPos = spawnPoints[i % spawnPoints.Count].position;
            GameObject worldObj = Instantiate(randomItem.worldPrefab, spawnPos, Quaternion.identity, spawnParent);

            // Assign unique ID to the pickup object
            string uniqueID = System.Guid.NewGuid().ToString();
            ItemScript pickup = worldObj.GetComponent<ItemScript>();
            if (pickup != null)
            {
                pickup.itemID = uniqueID;
            }

            // Store link between unique item and UI icon
            spawnedItems.Add(new ItemInstance
            {
                id = uniqueID,
                uiIcon = iconImage
            });
        }
    }

    public void CollectItem(string id)
    {
        foreach (var item in spawnedItems)
        {
            if (item.id == id)
            {
                item.uiIcon.color = Color.green;
                return;
            }
        }
    }
}

[System.Serializable]
public class ItemInstance
{
    public string id;
    public Image uiIcon;
}
