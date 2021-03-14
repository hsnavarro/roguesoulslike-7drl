using UnityEngine;
using System.Collections;

public class EnemyAttackWitch : EnemyAttack {

  [SerializeField]
  private GameObject projectilePrefab;

  private float animationDelay = 0.5f;
  private Vector3 offset = new Vector3(0.002f, 1.461f, 0.616f);

  public override void Attack() {
    StartCoroutine(InstantiateProjectile());
  }

  private IEnumerator InstantiateProjectile() {
    yield return new WaitForSeconds(animationDelay);
    GameObject projectile = Instantiate(projectilePrefab, transform.parent.parent.TransformPoint(offset), Quaternion.identity);
  }
}