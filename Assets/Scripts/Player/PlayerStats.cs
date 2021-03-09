using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
  public HP HP;

  public float maxStamina = 100f;
  public float normalSpeed = 5f;
  public float runSpeed = 10f;

  public float lightAttackDamage = 10f;
  public float heavyAttackDamage = 30f;

  public float lightAttackStaminaDecrease = 10f;
  public float heavyAttackStaminaDecrease = 30f;

  public float staminaRunDecreaseRate = 15f;
  public float staminaRechargeRate = 20f;
  public float staminaRechargeDelay = 5f;

  public float dashDuration = 0.5f;
  public float dashSpeed = 20f;

  public float Stamina;
  public float speed;
  public float damage;

  [HideInInspector]
  public bool isDashing;

  public void DebugInfo() {
    Debug.print("Player HP " + HP.currentHP);
    Debug.print("Player Stamina " + Stamina);
    Debug.print("Player isDashing " + isDashing);
  }

  private void Start() {
    Stamina = maxStamina;
    speed = normalSpeed;
  }

  private void Update() {
    //DebugInfo();
  }
}
