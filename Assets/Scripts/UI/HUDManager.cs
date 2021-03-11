using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
  public PlayerStats playerStats;

  public RectTransform healthBarRectangle;
  public RectTransform shieldBarRectangle;
  public RectTransform staminaBarRectangle;

  public Slider healthBarSlider;
  public Slider shieldBarSlider;
  public Slider staminaBarSlider;

  public List<Image> slotImages;
  [SerializeField]
  private Image slotImage1;
  [SerializeField]
  private Image slotImage2;
  [SerializeField]
  private Image slotImage3;
  [SerializeField]
  private Image slotImage4;

  private float GetHealthBarFillRatio() {
    return playerStats.defense.currentHealth / playerStats.defense.maxHealth;
  }

  private float GetShieldBarFillRatio() {
    return playerStats.defense.currentShield / playerStats.defense.maxShield;
  }

  private float GetStaminaBarFillRatio() {
    return playerStats.currentStamina / playerStats.maxStamina;
  }

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

  void Update() {
    healthBarRectangle.sizeDelta = new Vector2(playerStats.defense.maxHealth, healthBarRectangle.sizeDelta.y);
    shieldBarRectangle.sizeDelta = new Vector2(playerStats.defense.maxShield, shieldBarRectangle.sizeDelta.y);
    staminaBarRectangle.sizeDelta = new Vector2(playerStats.maxStamina, staminaBarRectangle.sizeDelta.y);

    healthBarSlider.value = GetHealthBarFillRatio();
    shieldBarSlider.value = GetShieldBarFillRatio();
    staminaBarSlider.value = GetStaminaBarFillRatio();
  }
}
