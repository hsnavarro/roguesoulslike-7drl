using UnityEngine;

public class EnemyWallTrigger : MonoBehaviour {
/*

  //[HideInInspector]
  public bool inAttackRange;

  private PlayerStats playerStats;

  [Header("Enemy References")]
  [SerializeField]
  private Animator enemyAnimator;
  [SerializeField]
  private EnemyStats enemyStats;
  [SerializeField]
  private EnemyAttack enemyAttack;
  [SerializeField]
  private CharacterController enemyCharacterController;

  private Vector3 enemyDirection;

  private bool isChasing = true;

  private int randomMovementCounter = 300;
  const int maxNumberOfSameMovement = 300;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    enemyDirection = new Vector3(0f, 0f, 0f);
  }

  private void ApplyMovement() {
    if (enemyAttack.isAttacking) return;
    enemyAnimator.SetBool("Moving", true);

    if (isChasing) {
      SetChaseDirection();
    } else {
      SetRandomDirection();
    }

    Debug.Log(enemyDirection);
    enemyCharacterController.Move(enemyDirection * enemyStats.speed * Time.deltaTime);
    enemyStats.transform.rotation = Quaternion.LookRotation(enemyDirection);
  }

  private void UpdateRandomDirection() {
    randomMovementCounter = 1;

    enemyDirection = Random.onUnitSphere;
    enemyDirection.y = 0;
    enemyDirection = enemyDirection.normalized;
  }

  private void SetRandomDirection() {
    if(randomMovementCounter == maxNumberOfSameMovement) {
      UpdateRandomDirection();
    } else {
      randomMovementCounter++;
    }
  }

  private void SetChaseDirection() {
    Vector3 direction = (playerStats.transform.position - transform.position);
    direction.y = 0;
    enemyDirection = direction.normalized;
  }

  private void OnTriggerEnter(Collider collider) {
    if (gameObject.tag == "WallDetectTrigger") {
      Debug.Log(gameObject.tag + " " + collider.gameObject.name);
        enemyDirection *= -1f;
        randomMovementCounter = 1;
    }

    //if (gameObject.tag == "AggroTrigger") isChasing = true;
  }

  private void OnTriggerStay(Collider collider) {
    if (gameObject.tag == "AttackTrigger") enemyAttack.TriggerAttack();
  }

  private void OnTriggerExit(Collider collider) {
    //if (gameObject.tag == "AggroTrigger") isChasing = false;      
  }

  private void Update() {
    ApplyMovement();
  }


  private void ApplyMovement() {
    // Debug.Log("In attack range " + inAttackRange);
    if (enemyAttack.isAttacking || inAttackRange) return;
    enemyAnimator.SetBool("Moving", true);

    if(playerStats) {
      Vector3 direction = (playerStats.transform.position - transform.position);
      direction.y = 0;
      enemyDirection = direction.normalized;

      enemyCharacterController.Move(enemyDirection * enemyStats.speed * Time.deltaTime);
      enemyStats.transform.rotation = Quaternion.LookRotation(enemyDirection);     
    }
  }

  private void OnControllerColliderHit(Collider collider) {
    Debug.Log(collider.name + " " + gameObject.name);
  }

  private void OnTriggerEnter(Collider collider) {
    if (gameObject.tag == "AttackTrigger") {
      Debug.Log("enter");
      inAttackRange = true;
    }
  }

  private void OnTriggerStay(Collider collider) {
    ApplyMovement();
  }

  private void OnTriggerExit(Collider collider) {
    if (gameObject.tag == "AggroTrigger") enemyAnimator.SetBool("Moving", false);
    if (gameObject.tag == "AttackTrigger") {
      inAttackRange = false;
      Debug.Log("exit");
    }
  }

  void Update() {
    Debug.Log("inAttackRange " + inAttackRange);
    if (inAttackRange)
      enemyAttack.TriggerAttack();

  }
  */
}
