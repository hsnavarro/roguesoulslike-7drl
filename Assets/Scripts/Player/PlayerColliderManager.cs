using UnityEngine;

public class PlayerColliderManager : MonoBehaviour {
  [SerializeField]
  private PlayerStats playerStats;

  private void OnControllerColliderHit(ControllerColliderHit hit) {
    // Flask
    if (hit.gameObject.layer == (int)Layers.FLASK) {
      if (playerStats.flasksCarried != playerStats.flasksCapacity) {
        playerStats.GetFlask();
        hit.gameObject.SetActive(false);
      }
    }
  }
}