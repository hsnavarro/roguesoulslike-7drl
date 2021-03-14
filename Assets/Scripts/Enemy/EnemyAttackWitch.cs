using UnityEngine;

public class EnemyAttackWitch : EnemyAttack {

  [SerializeField]
  private GameObject projectilePrefab;

  public override void Attack() {
    Debug.Log("attack");
    // Spawn Item
    Instantiate(projectilePrefab, transform.position, Quaternion.identity);
  }
}