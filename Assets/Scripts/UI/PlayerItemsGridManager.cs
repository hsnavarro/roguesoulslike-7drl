using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemsGridManager : MonoBehaviour {
  private PlayerStats playerStats;
  
  private List<Image> slotImages;
  private Image slotImage1;
  private Image slotImage2;
  private Image slotImage3;
  private Image slotImage4;

  void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  public void addSlotImage(int slot) {
    changeSlotImage(slot, true);
  }

  public void removeSlotImage(int slot) {
    changeSlotImage(slot, false);
  }

  public void changeSlotImage(int slot, bool isAdding) {
    if(slot >= PlayerStats.maxNumberOfItems) {
      Debug.Log("Slot number greater than array size");
      return;
    }

    if(!isAdding) {
      slotImages[slot].color = new Color(0f, 0f, 0f, 0f);
      return;
    }

    Image imageToAdd = playerStats.itemsEquipped[slot].GetComponent<Image>();
    if(isAdding) {
      slotImages[slot].color = imageToAdd.color;
    }
  }

  private void Awake() {
    slotImages = new List<Image>();
    slotImages.Add(GameObject.FindGameObjectWithTag("ItemImageSlot1").GetComponent<Image>());
    slotImages.Add(GameObject.FindGameObjectWithTag("ItemImageSlot2").GetComponent<Image>());
    slotImages.Add(GameObject.FindGameObjectWithTag("ItemImageSlot3").GetComponent<Image>());
    slotImages.Add(GameObject.FindGameObjectWithTag("ItemImageSlot4").GetComponent<Image>());
  }
}
