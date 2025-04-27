using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaveArea : MonoBehaviour
{
    // refrence to the item script
    public ItemUiSlot ItemUiSlot;

    // door text
    [SerializeField] TextMeshProUGUI LeaveDoorText;

    private void Start()
    {
        ItemUiSlot = FindAnyObjectByType<ItemUiSlot>(); // find script
    }

    private void Update()
    {
        if(ItemUiSlot.CurrentItemsFound != ItemUiSlot.MaxAmountItemsFound) // checks if the items collected is the amount in scene
        {
            LeaveDoorText.text = "Need Rest Items"; // changes text
        }
        else
        {
            LeaveDoorText.text = "Interact Leave Floor"; // changes text
        }
    }

    public void LeaveFloor() // runs when player interacts
    {
        if(ItemUiSlot.CurrentItemsFound != ItemUiSlot.MaxAmountItemsFound) // returns if number is not the same
        {
            return;
        }

        if(ItemUiSlot.CurrentItemsFound == ItemUiSlot.MaxAmountItemsFound) // player wins if number is the same
        {
            print("Level Won");
        }
    }
}
