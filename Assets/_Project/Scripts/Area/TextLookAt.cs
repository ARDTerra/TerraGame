using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TextLookAt : MonoBehaviour
{
    private Transform trans;
    private Vector3 offset = new Vector3(0, 180, 0);

    private void Start()
    {
        trans = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    private void Update()
    {
        transform.LookAt(trans);
        transform.Rotate(offset);
    }
}
