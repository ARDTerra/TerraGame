using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data for the item
[System.Serializable]
public class ItemData
{
    public string itemName; //Items name
    public Sprite icon; // the items ui image 
    public GameObject worldPrefab; // the item prefab
}
 