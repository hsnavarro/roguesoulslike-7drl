using UnityEngine;

public class WitchHealBehavior : StateMachineBehaviour {
    private EnemyAttackWitch enemyAttackWitch;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyAttackWitch = animator.transform.GetChild(0).GetComponentInChildren<EnemyAttackWitch>();
        enemyAttackWitch.lastTimeUsedHeal = Time.time;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyAttackWitch.enemyStats.enemyResilience.RecoverCurrentHealth(enemyAttackWitch.healthRecover);
        enemyAttackWitch.lastTimeUsedHeal = Time.time;
        enemyAttackWitch.isHealing = false;
        enemyAttackWitch.lastAttackTime = Time.time;
    }
}
