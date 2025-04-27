using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSet : MonoBehaviour
{
    [SerializeField] private GameObject detectUI;

    public GameObject SprintButton;
    public GameObject InteractButton;
    public GameObject Pos;

    private void Start()
    {
        SprintButton = GameObject.Find("SprintButton");
        InteractButton = GameObject.Find("InteractButton");
        Pos = GameObject.Find("ButtonPos");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            detectUI.transform.localPosition = new Vector3(0, 1, 0);
            detectUI.transform.GetChild(0).localScale = Vector3.one;

            if (FindAnyObjectByType<GameManager>().PhonePort == false)
            {
                return;
            }
            else
            {
                InteractButton.transform.localPosition = Pos.transform.localPosition;
                SprintButton.transform.localPosition = new Vector3(582, -7, 0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectUI.transform.localPosition = new Vector3(0, -3, 0);
            detectUI.transform.GetChild(0).localScale = Vector3.zero;

            if (FindAnyObjectByType<GameManager>().PhonePort == false)
            {
                return;
            }
            else
            {
                InteractButton.transform.localPosition = new Vector3(582, -7, 0);
                SprintButton.transform.localPosition = Pos.transform.localPosition;
            }
        }
    }
}
