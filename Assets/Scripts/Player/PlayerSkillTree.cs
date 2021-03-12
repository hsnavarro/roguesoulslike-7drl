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

  public int flasksCapacityIncrease;
  public float flasksCapacityBarProgression;
  public int flasksCapacityIncreasePerBarCompleted;
  public float flasksCapacityBarSize;

  public void GetPermanentInformation() {
    staminaIncrease = PermanentPlayerInformation.staminaIncrease;
    staminaBarProgression = PermanentPlayerInformation.staminaBarProgression;
    staminaIncreasePerBarCompleted = PermanentPlayerInformation.staminaIncreasePerBarCompleted;
    staminaBarSize = PermanentPlayerInformation.staminaBarSize;

    healthIncrease = PermanentPlayerInformation.healthIncrease;
    healthBarProgression = PermanentPlayerInformation.healthBarProgression;
    healthIncreasePerBarCompleted = PermanentPlayerInformation.healthIncreasePerBarCompleted;
    healthBarSize = PermanentPlayerInformation.healthBarSize;

    flasksCapacityIncrease = PermanentPlayerInformation.flasksCapacityIncrease;
    flasksCapacityBarProgression = PermanentPlayerInformation.flasksCapacityBarProgression;
    flasksCapacityIncreasePerBarCompleted = PermanentPlayerInformation.flasksCapacityIncreasePerBarCompleted;
    flasksCapacityBarSize = PermanentPlayerInformation.flasksCapacityBarSize;
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

    PermanentPlayerInformation.flasksCapacityIncrease = flasksCapacityIncrease;
    PermanentPlayerInformation.flasksCapacityBarProgression = flasksCapacityBarProgression;
    PermanentPlayerInformation.flasksCapacityIncreasePerBarCompleted = flasksCapacityIncreasePerBarCompleted;
    PermanentPlayerInformation.flasksCapacityBarSize = flasksCapacityBarSize;
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

  public void IncreaseFlasksCapacityProgress(float increase)
  {
    if (flasksCapacityBarProgression + increase >= flasksCapacityBarSize)
    {
      flasksCapacityIncrease += flasksCapacityIncreasePerBarCompleted;
      playerStats.flasksCapacity += flasksCapacityIncreasePerBarCompleted;
      float valueLeft = flasksCapacityBarProgression + increase - flasksCapacityBarSize;
      flasksCapacityBarProgression = valueLeft;
    }
    else
    {
      flasksCapacityBarProgression += increase;
    }
  }

}