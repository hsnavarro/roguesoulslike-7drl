using UnityEngine;

public class ItemPlayerDetection : MonoBehaviour {

  private PlayerItemUsage playerInteraction;

  private void Start() {
    playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItemUsage>();
  }

  private void OnTriggerEnter(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Item item = gameObject.GetComponent<Item>();
      playerInteraction.itemsInRange.Add(item);
    }
  }

  private void OnTriggerExit(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Item item = gameObject.GetComponent<Item>();
      playerInteraction.itemsInRange.Remove(item);
    }
  }
}