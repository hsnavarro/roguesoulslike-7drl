using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillTree : MonoBehaviour
{

  public PlayerStats playerStats;
  public float staminaIncrease;
  public float staminaBarProgression;
  public float staminaIncreasePerBarCompleted;
  public float staminaBarSize;

  public float healthIncrease;
  public float healthBarProgression;
  public float healthIncreasePerBarCompleted;
  public float healthBarSize;

  public float shieldIncrease;
  public float shieldBarProgression;
  public float shieldIncreasePerBarCompleted;
  public float shieldBarSize;

  public void GetPermanentInformation() {
    staminaIncrease = PermanentPlayerInformation.staminaIncrease;
    staminaBarProgression = PermanentPlayerInformation.staminaBarProgression;
    staminaIncreasePerBarCompleted = PermanentPlayerInformation.staminaIncreasePerBarCompleted;
    staminaBarSize = PermanentPlayerInformation.staminaBarSize;

    healthIncrease = PermanentPlayerInformation.healthIncrease;
    healthBarProgression = PermanentPlayerInformation.healthBarProgression;
    healthIncreasePerBarCompleted = PermanentPlayerInformation.healthIncreasePerBarCompleted;
    healthBarSize = PermanentPlayerInformation.healthBarSize;

    shieldIncrease = PermanentPlayerInformation.shieldIncrease;
    shieldBarProgression = PermanentPlayerInformation.shieldBarProgression;
    shieldIncreasePerBarCompleted = PermanentPlayerInformation.shieldIncreasePerBarCompleted;
    shieldBarSize = PermanentPlayerInformation.shieldBarSize;
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

    PermanentPlayerInformation.shieldIncrease = shieldIncrease;
    PermanentPlayerInformation.shieldBarProgression = shieldBarProgression;
    PermanentPlayerInformation.shieldIncreasePerBarCompleted = shieldIncreasePerBarCompleted;
    PermanentPlayerInformation.shieldBarSize = shieldBarSize;
  }
  public void IncreaseHealthProgress(float increase)
  {
    if (healthBarProgression + increase >= healthBarSize)
    {
      healthIncrease += healthIncreasePerBarCompleted;
      playerStats.defense.maxShield += healthIncreasePerBarCompleted;
      float valueLeft = healthBarProgression + increase - healthBarSize;
      healthBarProgression = valueLeft;
    }
    else
    {
      healthBarProgression += increase;
    }
  }

  public void IncreaseStaminaProgress(float increase)
  {
    if (staminaBarProgression + increase >= staminaBarSize)
    {
      staminaIncrease += staminaIncreasePerBarCompleted;
      playerStats.maxStamina += staminaIncreasePerBarCompleted;
      float valueLeft = staminaBarProgression + increase - staminaBarSize;
      staminaBarProgression = valueLeft;
    }
    else
    {
      staminaBarProgression += increase;
    }
  }

  public void IncreaseShieldProgress(float increase)
  {
    if (shieldBarProgression + increase >= shieldBarSize)
    {
      shieldIncrease += shieldIncreasePerBarCompleted;
      playerStats.defense.maxShield += shieldIncreasePerBarCompleted;
      float valueLeft = shieldBarProgression + increase - shieldBarSize;
      shieldBarProgression = valueLeft;
    }
    else
    {
      shieldBarProgression += increase;
    }
  }

}