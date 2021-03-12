using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {
  private PlayerItemInteraction playerInteraction;
  private void OnTriggerEnter(Collider collider) {

    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      NormalItem item = gameObject.GetComponent<NormalItem>();
      playerInteraction.itemsInRange.Add(item);
    }

  }

  private void OnTriggerExit(Collider collider) {

    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      NormalItem item = gameObject.GetComponent<NormalItem>();
      playerInteraction.itemsInRange.Remove(item);
    }

  }

  private void Start() {
    playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItemInteraction>();
  }

}