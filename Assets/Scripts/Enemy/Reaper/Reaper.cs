using UnityEngine;

public class Reaper : MonoBehaviour {

  [HideInInspector]
  public bool isAttacking;
  [HideInInspector]
  public bool isInvoking;
  [HideInInspector]
  public bool isTeleporting;
  [HideInInspector]
  public int enemysAlive;
  [HideInInspector]
  public int numberOfEnemysInvoked;

  private PlayerStats playerStats;

  [Header("Enemy References")]
  [SerializeField]
  private Animator reaperAnimator;
  [SerializeField]
  private EnemyStats reaperStats;
  [SerializeField]
  private CharacterController reaperCharacterController;

  private Vector3 reaperDirection;

  public void UpdateShouldInvoke() {
    if(enemysAlive == 0) {
      reaperAnimator.SetBool("ShouldInvoke", true);
    }
  }

  public void OnDeath() { 
    Object.Destroy(gameObject);
  }

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    reaperDirection = new Vector3(0f, 0f, 0f);
    reaperAnimator.SetBool("ShouldInvoke", false);
  }

  private void ApplyMovement() {
    if(isAttacking || isInvoking || isTeleporting) {
      return;
    }

    if(playerStats) {
      Vector3 direction = (playerStats.transform.position - transform.position);
      direction.y = 0;
      reaperDirection = direction.normalized;

      reaperCharacterController.Move(reaperDirection * reaperStats.speed * Time.deltaTime);
      reaperStats.transform.rotation = Quaternion.LookRotation(reaperDirection);     
    }
  }

  private void Update() {
    ApplyMovement();
  }
}