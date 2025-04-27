using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TextLookAt : MonoBehaviour
{
    private Transform cam; // cam transform refrence

   [SerializeField] private Vector3 offset = new Vector3(0, 180, 0); // offset

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>(); // sets cam
    }
    private void Update()
    {
        transform.LookAt(cam); // looks at cam
        transform.Rotate(offset); // rotates this object with offset
    }
}
