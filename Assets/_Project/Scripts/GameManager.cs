using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool PhonePort;

    public int MaxAmountItemsFound;
    public int CurrentItemsFound;

    public GameObject ItemUiParent;
    public GameObject ItemUI;

    [SerializeField]private GameObject PhoneButtons;

    private void Start()
    {
        for (int i = 0; i < MaxAmountItemsFound; i++)
        {
            var ItemUIObject = Instantiate(ItemUI, transform.position, transform.rotation);

            ItemUIObject.transform.SetParent(ItemUiParent.transform);
            ItemUIObject.transform.localScale = ItemUI.transform.localScale;

            ItemUIObject.SetActive(true);
        }
        if(PhonePort == false)
        {
            PhoneButtons.SetActive(false);
        }
    }
}
