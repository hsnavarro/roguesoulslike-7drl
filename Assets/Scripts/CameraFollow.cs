using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -5f);

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.5f;

    public bool isRotationOn;

    // LateUpdate is called once per frame, after Update
    void LateUpdate()
    {
        Vector3 destination = target.position + offset;
        Vector3 cameraPosition = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);

        transform.position = cameraPosition;
        if (isRotationOn) transform.LookAt(target.position);
    }
}
