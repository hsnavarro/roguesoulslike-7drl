using UnityEngine;

public class LightAttackBehavior : StateMachineBehaviour {
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController.IsLightAttacking = true;
        playerMovement.DecreaseStamina(playerStats.lightAttackStaminaDecrease);
        
        playerMovement.StartedAttacking();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerController.IsLightAttacking = false;
    }
}
