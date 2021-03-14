using UnityEngine;

public class ReaperTeleportBehavior : StateMachineBehaviour {
    private Reaper reaper;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        reaper = animator.transform.parent.GetComponent<Reaper>();
        reaper.Teleport();
    }
}
