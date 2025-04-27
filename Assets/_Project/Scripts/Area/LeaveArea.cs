using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaveArea : MonoBehaviour
{
    public ItemUiSlot ItemUiSlot;

    [SerializeField] TextMeshProUGUI LeaveDoorText;

    private void Start()
    {
        ItemUiSlot = FindAnyObjectByType<ItemUiSlot>();
    }

    private void Update()
    {
        if(ItemUiSlot.CurrentItemsFound != ItemUiSlot.MaxAmountItemsFound)
        {
            LeaveDoorText.text = "Need Rest Items";
        }
        else
        {
            LeaveDoorText.text = "Interact Leave Floor";
        }
    }

    public void LeaveFloor()
    {
        if(ItemUiSlot.CurrentItemsFound != ItemUiSlot.MaxAmountItemsFound)
        {
            return;
        }

        if(ItemUiSlot.CurrentItemsFound == ItemUiSlot.MaxAmountItemsFound)
        {
            print("Level Won");
        }
    }
}
