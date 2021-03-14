﻿using UnityEngine;

public class EnemyAggroTrigger : MonoBehaviour {
  private PlayerStats playerStats;

  [Header("Enemy References")]
  [SerializeField]
  private EnemyAttackTrigger enemyAttackTrigger;
  [SerializeField]
  private Animator enemyAnimator;
  [SerializeField]
  private EnemyStats enemyStats;
  [SerializeField]
  private EnemyAttack enemyAttack;
  [SerializeField]
  private CharacterController enemyCharacterController;

  private Vector3 enemyDirection;

  private bool isMoving = false;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  private void ApplyMovement() {
    if (enemyAttack.isAttacking || enemyAttackTrigger.inAttackRange) {
      enemyAnimator.SetBool("Moving", false);
      return;
    }
    enemyAnimator.SetBool("Moving", true);

    if(playerStats) {
      Vector3 direction = (playerStats.transform.position - transform.position);
      direction.y = 0;
      enemyDirection = direction.normalized;

      enemyCharacterController.Move(enemyDirection * enemyStats.speed * Time.deltaTime);
      enemyStats.transform.rotation = Quaternion.LookRotation(enemyDirection);     
    }
  }

  private void OnTriggerStay(Collider collider) {
    if (collider.gameObject.tag == "Player")
      isMoving = true;
  }

  private void OnTriggerExit(Collider collider) {
    if (collider.gameObject.tag == "Player")
      isMoving = false;
  }

  void FixedUpdate() {
    if (isMoving)
      ApplyMovement();
  }
}
