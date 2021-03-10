using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
  public Defense defense;
  public float currentDamage { get; set; }
  public float speed = 5f;
  public float attackDamage = 10f;
  public float attackDuration = 1f;
  public float attackImpactDelay = 0.2f;

  private bool attackImpactOccurred = false;
  public bool isAttacking { set; get; } = false;
  public float attackTimer { set; get; } = 0f;
  public void DebugInfo() {
    Debug.print("Enemy HP " + defense.currentHealth);
  }

  private void FixedUpdate() {
    if(isAttacking) {
      attackTimer += Time.fixedDeltaTime;
      
      if(attackImpactOccurred) {
        // Passed one frame, set currentDamage back to 0
        currentDamage = 0;
      } else if(attackTimer > attackImpactDelay) {
        currentDamage = attackDamage;
        attackImpactOccurred = true;
      }

      if (attackTimer > attackDuration) {
        isAttacking = false;
        attackImpactOccurred = false;
      }
    }
  }

  private void Update() {
    //DebugInfo();
  }
}