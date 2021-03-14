using UnityEngine;

public class ReaperHitboxTrigger : MonoBehaviour {

    [Header("Enemy References")]
    [SerializeField]
    private EnemyStats reaperStats;

    private void OnTriggerEnter(Collider collider) {
    if (gameObject.tag != "HitboxTrigger") return;

    if (collider.gameObject.layer == (int)Layers.PLAYER) {
      Resilience playerResilience = collider.gameObject.GetComponent<Resilience>();
      playerResilience.TakeDamage(reaperStats.attackDamage);
    }
  }
}