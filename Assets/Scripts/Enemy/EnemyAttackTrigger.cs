using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour {
  [HideInInspector]
  public bool inAttackRange;

  private PlayerStats playerStats;

  [Header("Enemy References")]
  [SerializeField]
  private EnemyAttack enemyAttack;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  private void OnTriggerEnter(Collider collider) {
    if (gameObject.tag == "AttackTrigger") {
      inAttackRange = true;
    }
  }

  private void OnTriggerStay(Collider collider) {
    if (gameObject.tag == "AttackTrigger") enemyAttack.TriggerAttack();
  }

  private void OnTriggerExit(Collider collider) {
    if (gameObject.tag == "AttackTrigger") {
      inAttackRange = false;
    }
  }
}
