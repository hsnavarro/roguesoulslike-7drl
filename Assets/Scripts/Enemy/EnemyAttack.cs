using UnityEngine;

public class EnemyAttack : MonoBehaviour {
  public Collider hitbox;
  [HideInInspector]
  public bool isAttacking = false;
  [HideInInspector]
  public float lastAttackTime;

  [Header("Enemy References")]
  public EnemyStats enemyStats;
  [SerializeField]
  private CharacterController enemyController;
  [SerializeField]
  public Animator enemyAnimator;

  private PlayerSkillTree playerSkillTree;

  // hsnavarro: do not remove this!
  public virtual void Attack() {
    enemyAnimator.SetTrigger("Attack");
  }

  public void TriggerAttack() {
    if(!isAttacking && Time.time - lastAttackTime >= enemyStats.attackDelay) {
      Attack();
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

    GameObject reaperGameObject = GameObject.FindGameObjectWithTag("Reaper");
    if (reaperGameObject) {
      Reaper reaper = reaperGameObject.GetComponent<Reaper>();
      reaper.enemysAlive--;

      if (reaper.enemysAlive == 0) reaper.UpdateShouldInvoke();
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
