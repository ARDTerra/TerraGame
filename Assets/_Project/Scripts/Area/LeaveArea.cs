using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaveArea : MonoBehaviour
{
    public GameManager Manager;

    [SerializeField] TextMeshProUGUI LeaveDoorText;

    private void Start()
    {
        Manager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if(Manager.CurrentItemsFound != Manager.MaxAmountItemsFound)
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
        if(Manager.CurrentItemsFound != Manager.MaxAmountItemsFound)
        {
            return;
        }

        if(Manager.CurrentItemsFound == Manager.MaxAmountItemsFound)
        {
            print("Level Won");
        }
    }
}
