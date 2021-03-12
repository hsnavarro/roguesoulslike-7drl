using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour {
  
  private ItemsManager itemsManager;
  public PlayerStats playerStats;
  [SerializeField]
  public List<NormalItem> itemsInRange;

  public float itemDropDistance = 3f;

  public void UseFlask() {
    if(playerStats.flasksCarried == 0) return;

    playerStats.defense.RecoverCurrentHealth(playerStats.flaskHealthIncrease);
    playerStats.defense.RecoverCurrentShield(playerStats.flaskShieldIncrease);
    playerStats.flasksCarried--;
  }

  private Material lastClosestItemMaterial = null;
  private NormalItem lastClosestItem = null;
  public void HandleInteractSlot(int slot) {
     if(slot >= PlayerStats.maxNumberOfItems) {
      Debug.Log("Slot number greater than array size");
      return;
    }

    if(itemsInRange.Count == 0) {
      Unequip(slot);
      return;
    }

     if(playerStats.isSlotEquipped[slot]) {
      Unequip(slot);
    }

    Equip(slot);
    
  }

  private void UpdateClosestItem() {

    float smallestDistanceToPlayer = Mathf.Infinity;
    NormalItem closestItemToPlayer = null;
    Material closestItemToPlayerMaterial = null;

    foreach (var item in itemsInRange) {

      float distanceToPlayer = (playerStats.transform.position - item.transform.position).magnitude;

      if(distanceToPlayer < smallestDistanceToPlayer) {
        smallestDistanceToPlayer = distanceToPlayer;
        closestItemToPlayer = item;
        closestItemToPlayerMaterial = item.gameObject.GetComponent<MeshRenderer>().material;
        bool isMaterialNull = closestItemToPlayerMaterial == null;
      }
    }

    if(closestItemToPlayer != lastClosestItem) {
      if (lastClosestItem != null) {
        Material material = lastClosestItem.gameObject.GetComponent<MeshRenderer>().material;

        material.DisableKeyword("_EMISSION");
      }

      lastClosestItem = closestItemToPlayer;
      lastClosestItemMaterial = closestItemToPlayerMaterial;

      if(closestItemToPlayer != null) {

        Material material = closestItemToPlayer.gameObject.GetComponent<MeshRenderer>().material;

        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", material.color);
      }
    }
  }
  private void Equip(int slot) {
    playerStats.itemsEquipped[slot] = lastClosestItem;
    playerStats.isSlotEquipped[slot] = true;
    itemsManager.addSlotImage(slot);
    
    lastClosestItem.AddEffects();
    lastClosestItem.gameObject.SetActive(false);
    itemsInRange.Remove(lastClosestItem);
  }

  public void Unequip(int slot) {
    if(!playerStats.isSlotEquipped[slot]) return;

    NormalItem item = playerStats.itemsEquipped[slot];

    item.RemoveEffects();

    Vector3 randomUpwardsVector = new Vector3(Random.Range(-1f, 1f), Random.value, Random.Range(-1f, 1f));
    randomUpwardsVector = randomUpwardsVector.normalized;

    item.gameObject.transform.position = playerStats.gameObject.transform.position + randomUpwardsVector * itemDropDistance;
    item.gameObject.SetActive(true);

    itemsManager.removeSlotImage(slot);
    playerStats.isSlotEquipped[slot] = false;
    playerStats.itemsEquipped[slot] = null;
  }

  private void Update() {
    UpdateClosestItem();
  }

  private void Start() {
    itemsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemsManager>();
  }
}