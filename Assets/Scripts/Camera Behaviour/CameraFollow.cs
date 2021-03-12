using UnityEngine;

public class CameraFollow : MonoBehaviour {
  private Transform target;
  private Vector3 offset;

  private Vector3 velocity = Vector3.zero;
  public float smoothTime = 0.5f;

  public bool isRotationOn;

  private void Start() {
    target = GameObject.FindGameObjectWithTag("CameraTarget").transform;

    offset = transform.position - target.position;
    transform.LookAt(target.position);
  }

  // LateUpdate is called once per frame, after Update
  void LateUpdate() {
    Vector3 destination = target.position + offset;
    Vector3 cameraPosition = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);

    transform.position = cameraPosition;
    if (isRotationOn) transform.LookAt(target.position);
  }
}
