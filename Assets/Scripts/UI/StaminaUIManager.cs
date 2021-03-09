using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUIManager : MonoBehaviour {
  public HUDManager HUDManager;
  public PlayerStats playerStats;
  public Slider staminaBar;
  public Text StaminaText;
  private float GetBarFillRatio() {
    return playerStats.Stamina / playerStats.maxStamina;
  }

  // Update is called once per frame
  void Update() {
    staminaBar.value = GetBarFillRatio();
    if (HUDManager.showText) StaminaText.text = Mathf.FloorToInt(playerStats.Stamina).ToString();
  }
}
