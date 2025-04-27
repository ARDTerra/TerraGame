using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Phone Settings")]
    public bool PhonePort;
    [SerializeField]private GameObject PhoneButtons;

    private void Start()
    {
        if(PhonePort == false) // sets phone buttons to false if not needed
        {
            PhoneButtons.SetActive(false);
        }
    }
}
