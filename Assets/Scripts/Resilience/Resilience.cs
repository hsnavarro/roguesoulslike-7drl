using UnityEngine;
using UnityEngine.Events;

public class Resilience : MonoBehaviour {
  [Header("Health Stats")]
  public float maxHealth = 100f;
  public float currentHealth = 100f;

  [Header("Shield Stats")]
  public float maxShield = 0f;
  public float currentShield = 0f;

  [SerializeField]
  private UnityEvent deathCallback;

  private float lastDamageTime = 0f;
  private float afterDamageInvulDuration = 0.5f;

  public void Awake() {
    currentHealth = maxHealth;
    currentShield = maxShield;
  }

  public void RecoverCurrentHealth(float value) {
    currentHealth = Mathf.Min(maxHealth, currentHealth + value);
  }

  public void RecoverCurrentShield(float value) {
    currentShield = Mathf.Min(maxShield, currentShield + value);
  }

  public void TakeDamage(float value) {
    if (currentHealth == 0f) return;

    Debug.Log("Taking damage " + gameObject.name);

    // Only positive values!
    if(value < 0f) return;

    // Invulnerable time
    if (Time.time - lastDamageTime < afterDamageInvulDuration)
      return;
    lastDamageTime = Time.time;

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
}
