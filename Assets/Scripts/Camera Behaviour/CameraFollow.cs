using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

  public Transform target;
  private Vector3 offset;

  private Vector3 velocity = Vector3.zero;
  public float smoothTime = 0.5f;

  public bool isRotationOn;

  private void Start() {
    offset = transform.position - target.position;
    transform.LookAt(target.position);
  }

  // LateUpdate is called once per frame, after Update
  void LateUpdate() {
    if(target == null) return;

    Vector3 destination = target.position + offset;
    Vector3 cameraPosition = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);

    transform.position = cameraPosition;
    if (isRotationOn) transform.LookAt(target.position);
  }
}
