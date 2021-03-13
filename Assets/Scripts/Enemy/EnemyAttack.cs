﻿using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
  [Header("Enemy References")]
  [SerializeField]
  private EnemyStats enemyStats;
  [SerializeField]
  private CharacterController enemyController;
  [SerializeField]
  private Animator enemyAnimator;

  private PlayerSkillTree playerSkillTree;

  // TEMPORARY until we have models
  public Collider hitbox;
  [SerializeField]
  private Material attackingMaterial;
  [SerializeField]
  private MeshRenderer meshRenderer;
  public bool isAttacking = false;
  private Material enemyMaterial;
  // ---

  [HideInInspector]
  public float lastAttackTime;

  public void OnDeath() {
    float valueToIncrease = enemyStats.barIncreaseAfterDeath;

    switch(enemyStats.enemyType) {
      case EnemyTypes.CYCLOPS:
        playerSkillTree.IncreaseHealthProgress(valueToIncrease);
        break;

      case EnemyTypes.WITCH:
        playerSkillTree.IncreaseFlasksCapacityProgress(valueToIncrease);
        break;

      case EnemyTypes.SATYR:
        playerSkillTree.IncreaseStaminaProgress(valueToIncrease);
        break;

      default:
        break;
    }

    Object.Destroy(transform.parent.gameObject);
  }

  private void Start() {
    playerSkillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillTree>();
  }

  public void TriggerAttack() {
    if(!isAttacking && Time.time - lastAttackTime >= enemyStats.attackDelay) {
      enemyAnimator.SetTrigger("Attack");
    }
  }

  private void OnTriggerEnter(Collider collider) {
    if (gameObject.tag != "HitboxTrigger") return;

    // TODO enemy attack enemy
    // hsnavarro: We are not doing this :/ 
    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Resilience playerResilience = collider.gameObject.GetComponent<Resilience>();
      playerResilience.TakeDamage(enemyStats.attackDamage);
    }
  }
}
