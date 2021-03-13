using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
  [Header("Enemy References")]
  [SerializeField]
  private EnemyStats enemyStats;
  [SerializeField]
  private CharacterController enemyController;

  private PlayerSkillTree playerSkillTree;

  // TEMPORARY until we have models
  [SerializeField]
  private Collider hitbox;
  [SerializeField]
  private Material attackingMaterial;
  [SerializeField]
  private MeshRenderer meshRenderer;
  private bool isAttacking = false;
  private Material enemyMaterial;
  // ---

  [HideInInspector]
  public float lastAttackTime;

  public void OnDeath() {
    float valueToIncrease = enemyStats.barIncreaseAfterDeath;

    switch(enemyStats.enemyType) {
      case EnemyTypes.CYCLOPS:
        playerSkillTree.IncreaseHealthProgress(valueToIncrease);
        break;

      case EnemyTypes.WITCH:
        playerSkillTree.IncreaseFlasksCapacityProgress(valueToIncrease);
        break;

      case EnemyTypes.FAUN:
        playerSkillTree.IncreaseStaminaProgress(valueToIncrease);
        break;

      default:
        break;
    }

    Object.Destroy(transform.parent.gameObject);
  }

  private void Start() {
    enemyMaterial = meshRenderer.material;
    playerSkillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillTree>();
  }

  public void TriggerAttack() {
    if(!isAttacking && Time.time - lastAttackTime >= enemyStats.attackDelay) {
      StartCoroutine(AttackCoroutine());
    }
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

  private IEnumerator AttackCoroutine() {
    hitbox.enabled = true;
    isAttacking = true;

    meshRenderer.material = attackingMaterial;

    yield return new WaitForSeconds(0.2f);

    lastAttackTime = Time.time;
    hitbox.enabled = false;
    isAttacking = false;

    meshRenderer.material = enemyMaterial;

    yield return null;
  }
}
