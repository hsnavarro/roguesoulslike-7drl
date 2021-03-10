using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
  public PlayerStats playerStats;
  public Collider playerCollider;
  public PlayerItemInteraction playerItemInteraction;
  private void OnTriggerStay(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.ENEMY) {
      Defense enemyDefense = collider.gameObject.GetComponent<Defense>();
      enemyDefense.TakeDamage(playerStats.currentDamage);
    }
  }
  private void OnControllerColliderHit(ControllerColliderHit hit) {

    // Edible Item 
    if (hit.gameObject.layer == (int)Layers.EDIBLE_ITEM) {
      EdibleItem item = hit.gameObject.GetComponent<EdibleItem>();
      playerItemInteraction.Use(item);
      hit.gameObject.SetActive(false);
    }
  }
}