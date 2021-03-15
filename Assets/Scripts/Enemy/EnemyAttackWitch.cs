using UnityEngine;
using System.Collections;

public class EnemyAttackWitch : EnemyAttack {

  [Header("Healing Stats")]
  public float percentageToHeal = 100f;
  public float delayTime = 10f;
  public float healthRecover = 10f;

  [HideInInspector]
  public bool isHealing;
  [HideInInspector]
  public float lastTimeUsedHeal = Mathf.NegativeInfinity;

  [SerializeField]
  private GameObject projectilePrefab;

  private float animationDelay = 0.5f;
  private Vector3 offset = new Vector3(0.002f, 1.461f, 0.616f);

  public override void Attack() {
    if (isHealing) return;
    enemyAnimator.SetTrigger("Attack");
    StartCoroutine(InstantiateProjectile());
  }

  private void Heal() {
    if ((enemyStats.enemyResilience.currentHealth / enemyStats.enemyResilience.maxHealth) * 100f <= percentageToHeal) {
      if (Time.time - lastTimeUsedHeal > delayTime) {
        isHealing = true;
        enemyAnimator.SetTrigger("Heal");
      }
    }
  }

  private IEnumerator InstantiateProjectile() {
    yield return new WaitForSeconds(animationDelay);
    GameObject projectile = Instantiate(projectilePrefab, transform.parent.parent.TransformPoint(offset), Quaternion.identity);
  }

  private void FixedUpdate() {
    Heal();
  }
}