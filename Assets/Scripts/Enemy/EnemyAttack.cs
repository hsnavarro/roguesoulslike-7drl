using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
  public EnemyStats enemyStats;

  public CharacterController enemyController;

  private PlayerSkillTree playerSkillTree;

  // TEMPORARY until we have models
  [SerializeField]
  private Collider hitbox;
  [SerializeField]
  private Material attackingMaterial;
  [SerializeField]
  private MeshRenderer meshRenderer;
  //private bool isAttacking = false;
  // ---

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

    Object.Destroy(gameObject);
  }

  private void Start() {
    StartCoroutine(AttackLoop());
    playerSkillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillTree>();
  }

  private void OnTriggerEnter(Collider collider) {
    // TODO enemy attack enemy
    // hsnavarro: We are not doing this :/ 
    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Resilience playerResilience = collider.gameObject.GetComponent<Resilience>();
      playerResilience.TakeDamage(enemyStats.attackDamage);
    }
  }

  private IEnumerator AttackLoop() {
    Material material = meshRenderer.material;

    while (true) {
      hitbox.enabled = true;
      //isAttacking = true;

      meshRenderer.material = attackingMaterial;

      yield return new WaitForSeconds(0.2f);

      hitbox.enabled = false;
      //isAttacking = false;

      meshRenderer.material = material;

      yield return new WaitForSeconds(1.0f);
    }
  }
}
