using UnityEngine;

public class EnemyAggroTriggerWitch : EnemyAggroTrigger {
  [Header("Enemy References")]
  [SerializeField]
  private EnemyAttackWitch enemyAttackWitch;

  public override void ApplyMovement() {
    if (enemyAttack.isAttacking || enemyAttackTrigger.inAttackRange || enemyAttackWitch.isHealing) {
      enemyAnimator.SetBool("Moving", false);
      return;
    }

    enemyAnimator.SetBool("Moving", true);

    if(playerStats) {
      Vector3 direction = (playerStats.transform.position - transform.position);
      direction.y = 0;
      enemyDesiredDirection = new Vector2(direction.x, direction.z).normalized;

      enemyCurrentDirection = Vector2.SmoothDamp(enemyCurrentDirection, enemyDesiredDirection, ref currentVelocity, smoothTime);

      Vector3 enemyMovement = new Vector3(enemyCurrentDirection.x, 0, enemyCurrentDirection.y).normalized;
      enemyCharacterController.Move(enemyMovement * enemyStats.speed * Time.deltaTime); 
      enemyStats.transform.rotation = Quaternion.LookRotation(enemyMovement); 
    }
  }
}
