using UnityEngine;

public class DashBehavior : StateMachineBehaviour {
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        
        playerMovement.lastTimeUsedDash = Time.time;
        playerMovement.gameObject.layer = (int)Layers.PLAYER_DASHING;
        playerMovement.isDashing = true;
        playerMovement.DecreaseStamina(playerStats.dashStaminaDecrease);
        playerMovement.dashDirection = playerMovement.playerDirection;

        playerMovement.StartedDash();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerMovement.gameObject.layer = (int)Layers.PLAYER;
        playerMovement.isDashing = false;
    }
}