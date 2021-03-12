using System.Collections;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour {
  public EnemyStats enemyStats;

  private PlayerSkillTree playerSkillTree;
  public CharacterController enemyController;

  // TEMPORARY until we have models
  public Collider hitbox;
  public Material attackingMaterial;
  public MeshRenderer meshRenderer;
  //private bool isAttacking = false;
  // ---

  void Start() {
    StartCoroutine(AttackLoop());
    playerSkillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillTree>();
  }

  private void OnTriggerEnter(Collider collider) {
    // TODO enemy attack enemy
    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Defense playerDefense = collider.gameObject.GetComponent<Defense>();
      playerDefense.TakeDamage(enemyStats.attackDamage);
    }
  }

  IEnumerator AttackLoop() {
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

  public void OnDeath() {
    float valueToIncrease = enemyStats.barIncreaseAfterDeath;

    switch(enemyStats.type) {
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
}
