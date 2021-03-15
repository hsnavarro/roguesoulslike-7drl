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
    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      inAttackRange = true;
    }
  }

  private void OnTriggerStay(Collider collider) {
    if (collider.gameObject.layer == (int)Layers.PLAYER)
      enemyAttack.TriggerAttack();
  }

  private void OnTriggerExit(Collider collider) {
    if (collider.gameObject.layer == (int)Layers.PLAYER)
      inAttackRange = false;
  }
}
