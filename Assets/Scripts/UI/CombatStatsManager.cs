using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatsManager : MonoBehaviour {
  private PlayerStats playerStats;

  [SerializeField]
  private RectTransform healthBarRectangle;
  [SerializeField]
  private RectTransform shieldBarRectangle;
  [SerializeField]
  private RectTransform staminaBarRectangle;

  [SerializeField]
  private Slider healthBarSlider;
  [SerializeField]
  private Slider shieldBarSlider;
  [SerializeField]
  private Slider staminaBarSlider;

  void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  private float GetHealthBarFillRatio() {
    return playerStats.defense.currentHealth / playerStats.defense.maxHealth;
  }

  private float GetShieldBarFillRatio() {
    return playerStats.defense.currentShield / playerStats.defense.maxShield;
  }

  private float GetStaminaBarFillRatio() {
    return playerStats.currentStamina / playerStats.maxStamina;
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
