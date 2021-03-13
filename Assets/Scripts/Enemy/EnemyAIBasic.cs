using UnityEngine;

public class EnemyAIBasic : MonoBehaviour {
  private PlayerStats playerStats;

  [SerializeField]
  private EnemyStats enemyStats;
  [SerializeField]
  private EnemyAttack enemyAttack;
  [SerializeField]
  private CharacterController enemyCharacterController;

  private Vector3 enemyDirection;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  private void UpdateDirection() {
    enemyDirection = (playerStats.transform.position - transform.position).normalized; 
  }

  private void ApplyMovement() {
    UpdateDirection();
    enemyCharacterController.Move(enemyDirection * enemyStats.speed * Time.deltaTime);
    Vector3 direction = playerStats.transform.position - transform.position;
    direction.y = 0f;

    enemyStats.transform.rotation = Quaternion.LookRotation(direction);
  }

  private void OnTriggerStay(Collider collider) {
    if (gameObject.tag == "AggroTrigger") ApplyMovement();
    if (gameObject.tag == "AttackTrigger") enemyAttack.TriggerAttack();
  }
}
