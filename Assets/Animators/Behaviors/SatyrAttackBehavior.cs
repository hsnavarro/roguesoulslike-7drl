using UnityEngine;

public class SatyrAttackBehavior : StateMachineBehaviour
{
    private EnemyAttack enemyAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyAttack = animator.transform.parent.GetComponentInChildren<EnemyAttack>();
        GameObject.FindGameObjectWithTag("HitboxTrigger").GetComponent<EnemyAttack>();
        enemyAttack.isAttacking = true;
        enemyAttack.hitbox.enabled = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyAttack.isAttacking = false;
        enemyAttack.hitbox.enabled = false;
        enemyAttack.lastAttackTime = Time.time;
    }
}
