using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Details")]
    public Transform target;

    [Header("Movement Settings")]
    public float smoothTime;
    public float rotationSpeed;

    [Header("Offsets")]
    public Vector3 offsetPos;
    public Vector3 offsetRot;

    private Vector3 velocity = Vector3.zero; //zeroed velocity

    void FixedUpdate()
    {
        if(target != null)
        {
            Vector3 targetPosition = target.position + offsetPos; // sets target pos and offset

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); // updates cam pos
            transform.rotation = Quaternion.Euler(offsetRot * rotationSpeed); // updates cam rot
        }
    }
}
