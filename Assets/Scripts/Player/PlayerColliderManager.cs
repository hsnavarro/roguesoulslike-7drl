using UnityEngine;

public class PlayerColliderManager : MonoBehaviour {
  [SerializeField]
  private PlayerStats playerStats;

  private void OnTriggerStay(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.ENEMY) {
      Resilience enemyResilience = collider.gameObject.GetComponent<Resilience>();
      enemyResilience.TakeDamage(playerStats.currentDamage);
    }
  }

  private void OnControllerColliderHit(ControllerColliderHit hit) {
    // Flask
    if (hit.gameObject.layer == (int)Layers.FLASK) {
      if (playerStats.flasksCarried == playerStats.flasksCapacity) return;

      playerStats.flasksCarried++;
      hit.gameObject.SetActive(false);
    }
  }
}