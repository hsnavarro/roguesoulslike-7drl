using UnityEngine;

public class ItemPlayerDetection : MonoBehaviour {

  private PlayerItemUsage playerItemUsage;

  private void Start() {
    playerItemUsage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItemUsage>();
    playerItemUsage.Start();
  }

  private void OnTriggerEnter(Collider collider) {
    if(gameObject.tag != "EmptyItem") return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Item item = gameObject.GetComponent<Item>();
      if (item != null && playerItemUsage != null && playerItemUsage.itemsInRange != null) 
        playerItemUsage.itemsInRange.Add(item);
    }
  }

  private void OnTriggerExit(Collider collider) {
    if(gameObject.tag != "EmptyItem") return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Item item = gameObject.GetComponent<Item>();
      playerItemUsage.itemsInRange.Remove(item);
    }
  }
}