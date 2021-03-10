using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour {
  public EnemyStats enemyStats;
  public CharacterController enemyController;

  private void OnTriggerStay(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Defense playerDefense = collider.gameObject.GetComponent<Defense>();
      playerDefense.TakeDamage(enemyStats.currentDamage);
    }
  }
  private void TriggerAttack() {
    if(enemyStats.isAttacking) return;
    
    enemyStats.isAttacking = true;
    enemyStats.attackTimer = 0f;
  }

  private void Update() {
    TriggerAttack();
  }
}