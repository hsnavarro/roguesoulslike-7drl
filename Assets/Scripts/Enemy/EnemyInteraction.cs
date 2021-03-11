using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour {
  public EnemyStats enemyStats;

  public PlayerSkillTree playerSkillTree;
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
     if(enemyStats.defense.IsDead()) {

       float valueToIncrease = enemyStats.barIncreaseAfterDeath;

      switch(enemyStats.type) {
        case EnemyTypes.WHICH:
          playerSkillTree.IncreaseHealthProgress(valueToIncrease);
          break;

        case EnemyTypes.CYCLOPS:
          playerSkillTree.IncreaseShieldProgress(valueToIncrease);
          break;

        case EnemyTypes.FAUN:
          playerSkillTree.IncreaseStaminaProgress(valueToIncrease);
          break;


        default:
          break;
      
      }
       Object.Destroy(gameObject);
     }
  }
}