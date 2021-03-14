using UnityEngine;

public class ReaperTeleportBehavior : StateMachineBehaviour
{
    private Reaper reaper;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        reaper = animator.transform.parent.GetComponent<Reaper>();
        reaper.isTeleporting = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        reaper.isTeleporting = false;
    }
}
