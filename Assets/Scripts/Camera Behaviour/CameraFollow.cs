using UnityEngine;

public class CameraFollow : MonoBehaviour {
  private Transform cameraTarget;
  private Vector3 offset;

  private Vector3 velocity = Vector3.zero;

  [SerializeField]
  private float smoothTime = 0.5f;

  [SerializeField]
  private bool isRotationOn;

  private void Start() {
    cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").transform;

    offset = transform.position - cameraTarget.position;
    transform.LookAt(cameraTarget.position);
  }

  // LateUpdate is called once per frame, after Update
  private void LateUpdate() {
    Vector3 finalPosition = cameraTarget.position + offset;
    Vector3 cameraPosition = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothTime);

    transform.position = cameraPosition;
    if (isRotationOn) transform.LookAt(cameraTarget.position);
  }

  public void Teleport() {
    transform.position = cameraTarget.position + offset;
  }
}
