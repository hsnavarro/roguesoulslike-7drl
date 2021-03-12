using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatStatsManager : MonoBehaviour {
  private PlayerStats playerStats;

  private RectTransform playerHealthBarRectangle;
  private Slider playerHealthBarSlider;

  /*
  [Header("Player Shield Bar References")]
  [SerializeField]
  private RectTransform playerShieldBarRectangle;
  [SerializeField]
  private Slider playerShieldBarSlider;
  */

  private RectTransform playerStaminaBarRectangle;
  private Slider playerStaminaBarSlider;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

    playerHealthBarRectangle = GameObject.FindGameObjectWithTag("HealthBarPlayer").GetComponent<RectTransform>();
    playerHealthBarSlider = GameObject.FindGameObjectWithTag("HealthBarPlayer").GetComponent<Slider>();

    playerStaminaBarRectangle = GameObject.FindGameObjectWithTag("StaminaBarPlayer").GetComponent<RectTransform>();
    playerStaminaBarSlider = GameObject.FindGameObjectWithTag("StaminaBarPlayer").GetComponent<Slider>();
  }

  private float GetHealthBarFillRatio() {
    return playerStats.playerResilience.currentHealth / playerStats.playerResilience.maxHealth;
  }

  private float GetShieldBarFillRatio() {
    return playerStats.playerResilience.currentShield / playerStats.playerResilience.maxShield;
  }

  private float GetStaminaBarFillRatio() {
    return playerStats.currentStamina / playerStats.maxStamina;
  }

  private void Update() {
    playerHealthBarRectangle.sizeDelta = new Vector2(playerStats.playerResilience.maxHealth, playerHealthBarRectangle.sizeDelta.y);
    //playerShieldBarRectangle.sizeDelta = new Vector2(playerStats.playerResilience.maxShield, playerShieldBarRectangle.sizeDelta.y);
    playerStaminaBarRectangle.sizeDelta = new Vector2(playerStats.maxStamina, playerStaminaBarRectangle.sizeDelta.y);

    playerHealthBarSlider.value = GetHealthBarFillRatio();
    //playerShieldBarSlider.value = GetShieldBarFillRatio();
    playerStaminaBarSlider.value = GetStaminaBarFillRatio();
  }
}
