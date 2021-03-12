using UnityEngine;

public class PlayerColliderEvents : MonoBehaviour {
  public PlayerStats playerStats;
  private void OnTriggerStay(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.ENEMY) {
      Defense enemyDefense = collider.gameObject.GetComponent<Defense>();
      enemyDefense.TakeDamage(playerStats.currentDamage);
    }
  }
  private void OnControllerColliderHit(ControllerColliderHit hit) {

    // Edible Item 
    if (hit.gameObject.layer == (int)Layers.FLASK) {
      if (playerStats.flasksCarried == playerStats.flasksCapacity) return;

      playerStats.flasksCarried++;
      hit.gameObject.SetActive(false);
    }
  }
}