using UnityEngine;

public class ReaperAttackTrigger : MonoBehaviour {
  [SerializeField]
  private Animator reaperAnimator;
  public Reaper reaper;

  private void OnTriggerStay(Collider collider) {
    if (collider.gameObject.tag == "Player" && !reaper.isAttacking && !reaper.isTeleporting && !reaper.isInvoking)
      reaperAnimator.SetTrigger("Attack");
  }
}