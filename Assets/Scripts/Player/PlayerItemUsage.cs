using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUsage : MonoBehaviour {
  [HideInInspector]
  public List<Item> itemsInRange;

  [SerializeField]
  private PlayerStats playerStats;
  private PlayerItemsGridManager playerItemsGridManager;

  [Header("Item Drop Distance")]
  public float itemDropDistance = 3f;

  private Material lastClosestItemMaterial = null;
  private Item lastClosestItem = null;

  public void UseFlask() {
    if(playerStats.flasksCarried == 0) return;

    playerStats.playerResilience.RecoverCurrentHealth(playerStats.flaskHealthIncrease);
    playerStats.playerResilience.RecoverCurrentShield(playerStats.flaskShieldIncrease);
    playerStats.flasksCarried--;
  }

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

  public void Unequip(int slot) {
    if(!playerStats.isSlotEquipped[slot]) return;

    Item item = playerStats.itemsEquipped[slot];

    item.RemoveEffects();

    Vector3 randomUpwardsVector = new Vector3(Random.Range(-1f, 1f), Random.value, Random.Range(-1f, 1f));
    randomUpwardsVector = randomUpwardsVector.normalized;

    item.gameObject.transform.position = playerStats.gameObject.transform.position + randomUpwardsVector * itemDropDistance;
    item.gameObject.SetActive(true);

    playerItemsGridManager.removeSlotImage(slot);
    playerStats.isSlotEquipped[slot] = false;
    playerStats.itemsEquipped[slot] = null;
  }

  private void Start() {
    playerItemsGridManager = GameObject.FindGameObjectWithTag("ImagesItemsGrid").GetComponent<PlayerItemsGridManager>();
  }

  private void UpdateClosestItem() {
    float smallestDistanceToPlayer = Mathf.Infinity;
    Item closestItemToPlayer = null;
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
        ItemDescriptionManager itemUI = lastClosestItem.gameObject.GetComponent<ItemDescriptionManager>();
        itemUI.Hide();

        material.DisableKeyword("_EMISSION");
      }

      lastClosestItem = closestItemToPlayer;
      lastClosestItemMaterial = closestItemToPlayerMaterial;

      if(closestItemToPlayer != null) {

        Material material = closestItemToPlayer.gameObject.GetComponent<MeshRenderer>().material;
        ItemDescriptionManager itemUI = lastClosestItem.gameObject.GetComponent<ItemDescriptionManager>();
        itemUI.Show();

        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", material.color);
      }
    }
  }

  private void Equip(int slot) {
    playerStats.itemsEquipped[slot] = lastClosestItem;
    playerStats.isSlotEquipped[slot] = true;
    playerItemsGridManager.addSlotImage(slot);
    
    lastClosestItem.AddEffects();
    lastClosestItem.gameObject.SetActive(false);
    itemsInRange.Remove(lastClosestItem);
  }

  private void Update() {
    UpdateClosestItem();
  }
}