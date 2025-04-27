using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSet : MonoBehaviour
{
    [SerializeField] private GameObject detectUI; // detect UI text

    public GameObject SprintButton;
    public GameObject InteractButton;
    public GameObject Pos;

    private void Start() // sets all buttons
    {
        SprintButton = GameObject.Find("SprintButton");
        InteractButton = GameObject.Find("InteractButton");
        Pos = GameObject.Find("ButtonPos");
    }

    private void OnTriggerEnter(Collider other) // if player is within range with something that can be interacted with
    {
        if (other.tag == "Player")
        {
            detectUI.transform.localPosition = new Vector3(0, 1, 0); // changes pos and scale of text
            detectUI.transform.GetChild(0).localScale = Vector3.one;

            if (FindAnyObjectByType<GameManager>().PhonePort == false) // checks if game ment to have buttonsn for phone
            {
                return;
            }
            else
            {
                // if needs buttons it will swap sprint with interact
                InteractButton.transform.localPosition = Pos.transform.localPosition;
                SprintButton.transform.localPosition = new Vector3(582, -7, 0);
            }
        }
    }
    private void OnTriggerExit(Collider other) // if player leaves the area
    {
        if (other.tag == "Player")
        {
            detectUI.transform.localPosition = new Vector3(0, -3, 0); // changes pos and scale of text
            detectUI.transform.GetChild(0).localScale = Vector3.zero;

            if (FindAnyObjectByType<GameManager>().PhonePort == false) // checks if game ment to have buttons for phone
            {
                return;
            }
            else
            {
                // if needs buttons it will spawn interact with sprint
                InteractButton.transform.localPosition = new Vector3(582, -7, 0);
                SprintButton.transform.localPosition = Pos.transform.localPosition;
            }
        }
    }
}
