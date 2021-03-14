using UnityEngine;

public class ReaperAttackTrigger : MonoBehaviour {
  [SerializeField]
  private Animator reaperAnimator;

  private void OnTriggerStay(Collider collider) {
    if (gameObject.tag == "AttackTrigger") reaperAnimator.SetTrigger("Attack");
  }
}