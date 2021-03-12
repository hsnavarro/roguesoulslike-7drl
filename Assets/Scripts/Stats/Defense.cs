using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Defense : MonoBehaviour {
  public float maxHealth = 100f;
  public float currentHealth = 100f;

  public float maxShield = 0f;
  public float currentShield = 0f;

  public UnityEvent deathCallback;

  public void RecoverCurrentHealth(float value) {
    currentHealth = Mathf.Min(maxHealth, currentHealth + value);
  }

  public void RecoverCurrentShield(float value) {
    currentShield = Mathf.Min(maxShield, currentShield + value);
  }

  public void ChangeMaxHealth(float value) {
    maxHealth += value;
    maxHealth = Mathf.Max(0f, maxHealth + value);
  }

  public void ChangeMaxShield(float value) {
    maxShield += value;
    maxShield = Mathf.Max(0f, maxShield + value);
  }

  public void TakeDamage(float value) {

    if (currentHealth == 0f) return;

    // Only positive values!
    if(value < 0f) return;

    if (value < currentShield) {
      currentShield -= value;
      value = 0f;
    } else {
      value -= currentShield;
      currentShield = 0f;
    }

    currentHealth = Mathf.Max(0f, currentHealth - value);
    if (currentHealth == 0f) {
      if (deathCallback != null) {
        deathCallback.Invoke();
      } 
    }
  }

  public void Awake() {
    currentHealth = maxHealth;
    currentShield = maxShield;
  }
}
