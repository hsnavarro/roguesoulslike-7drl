using UnityEngine;

public class PlayerSkillTree : MonoBehaviour {
  [Header("Stamina Progress Stats")]
  public float staminaIncrease;
  public float staminaBarProgression;
  public float staminaIncreasePerBarCompleted;
  public float staminaBarSize;

  [Header("Health Progress Stats")]
  public float healthIncrease;
  public float healthBarProgression;
  public float healthIncreasePerBarCompleted;
  public float healthBarSize;

  [Header("Flask Progress Stats")]
  public int flasksHealIncrease;
  public float flasksHealBarProgression;
  public int flasksHealIncreasePerBarCompleted;
  public float flasksHealBarSize;

  [SerializeField]
  private PlayerStats playerStats;

  [SerializeField]
  private PlayerSkillTreeManager hudManager;

  public void GetPermanentInformation() {
    staminaIncrease = PermanentPlayerInformation.staminaIncrease;
    staminaBarProgression = PermanentPlayerInformation.staminaBarProgression;
    staminaIncreasePerBarCompleted = PermanentPlayerInformation.staminaIncreasePerBarCompleted;
    staminaBarSize = PermanentPlayerInformation.staminaBarSize;

    healthIncrease = PermanentPlayerInformation.healthIncrease;
    healthBarProgression = PermanentPlayerInformation.healthBarProgression;
    healthIncreasePerBarCompleted = PermanentPlayerInformation.healthIncreasePerBarCompleted;
    healthBarSize = PermanentPlayerInformation.healthBarSize;

    flasksHealIncrease = PermanentPlayerInformation.flasksHealIncrease;
    flasksHealBarProgression = PermanentPlayerInformation.flasksHealBarProgression;
    flasksHealIncreasePerBarCompleted = PermanentPlayerInformation.flasksHealIncreasePerBarCompleted;
    flasksHealBarSize = PermanentPlayerInformation.flasksHealBarSize;
  }

  public void SetPermanentInformation() {
    PermanentPlayerInformation.staminaIncrease = staminaIncrease;
    PermanentPlayerInformation.staminaBarProgression = staminaBarProgression;
    PermanentPlayerInformation.staminaIncreasePerBarCompleted = staminaIncreasePerBarCompleted;
    PermanentPlayerInformation.staminaBarSize = staminaBarSize;

    PermanentPlayerInformation.healthIncrease = healthIncrease;
    PermanentPlayerInformation.healthBarProgression = healthBarProgression;
    PermanentPlayerInformation.healthIncreasePerBarCompleted = healthIncreasePerBarCompleted;
    PermanentPlayerInformation.healthBarSize = healthBarSize;

    PermanentPlayerInformation.flasksHealIncrease = flasksHealIncrease;
    PermanentPlayerInformation.flasksHealBarProgression = flasksHealBarProgression;
    PermanentPlayerInformation.flasksHealIncreasePerBarCompleted = flasksHealIncreasePerBarCompleted;
    PermanentPlayerInformation.flasksHealBarSize = flasksHealBarSize;
  }

  public void IncreaseHealthProgress(float increase) {
    if (healthBarProgression + increase >= healthBarSize) {
      healthIncrease += healthIncreasePerBarCompleted;
      playerStats.playerResilience.maxHealth += healthIncreasePerBarCompleted;
      float valueLeft = healthBarProgression + increase - healthBarSize;
      healthBarProgression = valueLeft;

      playerStats.GetFlask();
      hudManager.HealthLevelUp();
    } else {
      healthBarProgression += increase;
    }
  }

  public void IncreaseStaminaProgress(float increase) {
    if (staminaBarProgression + increase >= staminaBarSize) {
      staminaIncrease += staminaIncreasePerBarCompleted;
      playerStats.maxStamina += staminaIncreasePerBarCompleted;
      float valueLeft = staminaBarProgression + increase - staminaBarSize;
      staminaBarProgression = valueLeft;

      playerStats.GetFlask();
      hudManager.StaminaLevelUp();
    } else {
      staminaBarProgression += increase;
    }
  }

  public void IncreaseFlasksHealProgress(float increase) {
    if (flasksHealBarProgression + increase >= flasksHealBarSize) {
      flasksHealIncrease += flasksHealIncreasePerBarCompleted;
      playerStats.flaskHeal += flasksHealIncreasePerBarCompleted;
      float valueLeft = flasksHealBarProgression + increase - flasksHealBarSize;
      flasksHealBarProgression = valueLeft;

      playerStats.GetFlask();
      hudManager.FlaskLevelUp();
    } else {
      flasksHealBarProgression += increase;
    }
  }

  private void Awake() {
    GetPermanentInformation();
    playerStats.playerResilience.maxHealth += PermanentPlayerInformation.healthIncrease;
    playerStats.flaskHeal += PermanentPlayerInformation.flasksHealIncrease;
    playerStats.maxStamina += PermanentPlayerInformation.staminaIncrease;
  }

}