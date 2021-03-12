using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour {
  public PlayerStats playerStats;

  [SerializeField]
  private List<Image> slotImages;
  [SerializeField]
  private Image slotImage1;
  [SerializeField]
  private Image slotImage2;
  [SerializeField]
  private Image slotImage3;
  [SerializeField]
  private Image slotImage4;

  public void addSlotImage(int slot) {
    changeSlotImage(slot, true);
  }

  public void removeSlotImage(int slot) {
    changeSlotImage(slot, false);
  }

  public void changeSlotImage(int slot, bool isAdding) {
    if(slot >= PlayerStats.maxNumberOfItems) {
      Debug.print("Slot number greater than array size");
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

  void Awake() {
    slotImages.Add(slotImage1);
    slotImages.Add(slotImage2);
    slotImages.Add(slotImage3);
    slotImages.Add(slotImage4);
  }
}
