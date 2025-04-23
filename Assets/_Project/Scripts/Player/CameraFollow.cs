using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime;
    public Vector3 offsetPos;

    public Vector3 offsetRot;
    public float rotationSpeed;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null)
        {
            Vector3 targetPosition = target.position + offsetPos;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            transform.rotation = Quaternion.Euler(offsetRot * rotationSpeed);
        }
    }
}
