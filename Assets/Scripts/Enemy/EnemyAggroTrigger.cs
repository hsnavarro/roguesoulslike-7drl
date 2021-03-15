using UnityEngine;

public class EnemyAggroTrigger : MonoBehaviour {
  [HideInInspector]
  public Vector2 enemyCurrentDirection;
  [HideInInspector]
  public Vector2 enemyDesiredDirection;

  [HideInInspector]
  public Vector2 currentVelocity;

  [HideInInspector]
  public bool isMoving = false;

  [HideInInspector]
  public PlayerStats playerStats;

  [Header("Enemy References")]
  public EnemyAttackTrigger enemyAttackTrigger;
  public Animator enemyAnimator;
  public EnemyStats enemyStats;
  public EnemyAttack enemyAttack;
  public CharacterController enemyCharacterController;
  public float smoothTime = 0.5f;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
  }

  public virtual void ApplyMovement() {
    if (enemyAttack.isAttacking || enemyAttackTrigger.inAttackRange) {
      enemyAnimator.SetBool("Moving", false);
      return;
    }

    enemyAnimator.SetBool("Moving", true);

    if(playerStats) {
      Vector3 direction = (playerStats.transform.position - transform.position);
      direction.y = 0;
      enemyDesiredDirection = new Vector2(direction.x, direction.z).normalized;

      enemyCurrentDirection = Vector2.SmoothDamp(enemyCurrentDirection, enemyDesiredDirection, ref currentVelocity, smoothTime);

      Vector3 enemyMovement = new Vector3(enemyCurrentDirection.x, 0, enemyCurrentDirection.y).normalized;
      enemyCharacterController.Move(enemyMovement * enemyStats.speed * Time.deltaTime); 
      enemyStats.transform.rotation = Quaternion.LookRotation(enemyMovement); 
    }
  }

  private void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.tag == "Player")
      isMoving = true;
  }

  private void OnTriggerExit(Collider collider) {
    if (collider.gameObject.tag == "Player") {
      isMoving = false;
      enemyAnimator.SetBool("Moving", false);
    }
  }

  void FixedUpdate() {
    if (isMoving)
      ApplyMovement();
  }
}
