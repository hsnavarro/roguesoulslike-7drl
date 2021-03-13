using UnityEngine;

public class ItemPlayerDetection : MonoBehaviour {

  private PlayerItemUsage playerItemUsage;

  private void Start() {
    playerItemUsage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItemUsage>();
  }

  private void OnTriggerEnter(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Item item = gameObject.GetComponent<Item>();
      playerItemUsage.itemsInRange.Add(item);
    }
  }

  private void OnTriggerExit(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Item item = gameObject.GetComponent<Item>();
      playerItemUsage.itemsInRange.Remove(item);
    }
  }
}