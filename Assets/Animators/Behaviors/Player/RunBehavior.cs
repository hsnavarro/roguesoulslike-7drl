using UnityEngine;

public class RunBehavior : StateMachineBehaviour {
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerMovement.DecreaseStamina(Time.deltaTime * playerStats.staminaRunDecreaseRate);
        playerStats.currentSpeed = playerStats.runSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerMovement.staminaLastTimeUsed = Time.time;
        playerMovement.DecreaseStamina(Time.deltaTime * playerStats.staminaRunDecreaseRate);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerMovement.staminaLastTimeUsed = Time.time;
        playerMovement.DecreaseStamina(Time.deltaTime * playerStats.staminaRunDecreaseRate);
        playerStats.currentSpeed = playerStats.normalSpeed;
    }
}
