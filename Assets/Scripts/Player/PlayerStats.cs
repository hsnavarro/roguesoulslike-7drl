using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

  [HideInInspector]
  public const int maxNumberOfItems = 4;
  //[HideInInspector]
  public NormalItem[] itemsEquipped;
  //[HideInInspector]
  public bool[] isSlotEquipped;

  public Defense defense;

  public int flasksCapacity = 3;
  public int flasksCarried = 0;
  public float flaskShieldIncrease = 0f;
  public float flaskHealthIncrease = 10f;

  public float maxStamina = 100f;
  public float normalSpeed = 5f;
  public float runSpeed = 10f;

  public float lightAttackDamage = 10f;
  public float lightAttackDuration = 0.2f;
  public float lightAttackImpactDelay = 0f;
  private bool lightAttackImpactOccurred = false;
  [HideInInspector]
  public bool isLightAttacking = false;
  [HideInInspector]
  public float lightAttackTimer = 0f;

  public float heavyAttackDamage = 30f;
  public float heavyAttackDuration = 0.3f;
  public float heavyAttackImpactDelay = 0.2f;

  private bool heavyAttackImpactOccurred = false;
  [HideInInspector]
  public bool isHeavyAttacking = false;
  [HideInInspector]
  public float heavyAttackTimer = 0f;

  public float lightAttackStaminaDecrease = 10f;
  public float heavyAttackStaminaDecrease = 30f;

  public float staminaRunDecreaseRate = 15f;
  public float staminaRechargeRate = 20f;
  public float staminaRechargeDelay = 1f;

  public float dashDuration = 0.5f;
  public float dashSpeed = 20f;
  public float dashStaminaDecrease = 10f;

  public float currentStamina;
  public float currentSpeed;
  public float currentDamage;

  [HideInInspector]
  public bool isDashing;

  public void DebugInfo() {
    //Debug.Log("Player Health " + defense.currentHealth);
    //Debug.Log("Player Shield " + defense.currentShield);
    //Debug.Log("Player Stamina " + currentStamina);
    Debug.Log("Player Damage " + currentDamage * Time.fixedDeltaTime);
    //Debug.Log("Player isDashing " + isDashing);
  }

  private void Start() {
    itemsEquipped = new NormalItem[maxNumberOfItems];
    isSlotEquipped = new bool[maxNumberOfItems];
    currentStamina = maxStamina;
    currentSpeed = normalSpeed;
  }

  private void FixedUpdate() {
    if(isHeavyAttacking) {
      heavyAttackTimer += Time.fixedDeltaTime;
      
      if(heavyAttackImpactOccurred) {
        // Passed one frame, set currentDamage back to 0
        currentDamage = 0;
      } else if(heavyAttackTimer > heavyAttackImpactDelay) {
        currentDamage = heavyAttackDamage;
        heavyAttackImpactOccurred = true;
      }

      if (heavyAttackTimer > heavyAttackDuration) {
        isHeavyAttacking = false;
        heavyAttackImpactOccurred = false;
      }
    }

    if(isLightAttacking) {
      lightAttackTimer += Time.fixedDeltaTime;

      if(lightAttackImpactOccurred) {
        // Passed one frame, set currentDamage back to 0
        currentDamage = 0;
      } else if(lightAttackTimer > lightAttackImpactDelay) {
        currentDamage = lightAttackDamage;
        lightAttackImpactOccurred = true;
      }

      if (lightAttackTimer > lightAttackDuration) {
        isLightAttacking = false;
        lightAttackImpactOccurred = false;
      }
    }
  }
  private void Update() {
    // DebugInfo();
  }
}
