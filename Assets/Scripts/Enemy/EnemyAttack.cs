using UnityEngine;

public class EnemyAttack : MonoBehaviour {
  public Collider hitbox;
  [HideInInspector]
  public bool isAttacking = false;
  [HideInInspector]
  public float lastAttackTime;

  [Header("Enemy References")]
  [SerializeField]
  private EnemyStats enemyStats;
  [SerializeField]
  private CharacterController enemyController;
  [SerializeField]
  private Animator enemyAnimator;

  private PlayerSkillTree playerSkillTree;

  // hsnavarro: do not remove this!
  public virtual void Attack() {

  }

  public void TriggerAttack() {
    if(!isAttacking && Time.time - lastAttackTime >= enemyStats.attackDelay) {
      enemyAnimator.SetTrigger("Attack");
    }
  }

  public void OnDeath() {
    float valueToIncrease = enemyStats.rewardAmount;

    switch(enemyStats.reward) {
      case EnemyStats.RewardType.Health:
        playerSkillTree.IncreaseHealthProgress(valueToIncrease);
        break;

      case EnemyStats.RewardType.Flask:
        playerSkillTree.IncreaseFlasksCapacityProgress(valueToIncrease);
        break;

      case EnemyStats.RewardType.Stamina:
        playerSkillTree.IncreaseStaminaProgress(valueToIncrease);
        break;

      default:
        break;

    }

    Object.Destroy(transform.parent.parent.parent.gameObject);
  }

  private void Start() {
    playerSkillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillTree>();
  }

  private void OnTriggerEnter(Collider collider) {
    if (gameObject.tag != "HitboxTrigger") return;

    // TODO enemy attack enemy
    // hsnavarro: We are not doing this :/ 
    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Resilience playerResilience = collider.gameObject.GetComponent<Resilience>();
      playerResilience.TakeDamage(enemyStats.attackDamage);
    }
  }
}
