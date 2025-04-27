using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool PhonePort;
    [SerializeField]private GameObject PhoneButtons;

    private void Start()
    {
       
        if(PhonePort == false)
        {
            PhoneButtons.SetActive(false);
        }
    }
}
