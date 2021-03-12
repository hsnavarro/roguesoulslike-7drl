using UnityEngine;

public class EnemyStats : MonoBehaviour {
  public EnemyTypes enemyType;
  public Resilience enemyResilience;

  [Header("Player Progress Bar Stats")]
  public float barIncreaseAfterDeath = 1f;
  
  [Header("Speed Stats")]
  public float speed = 5f;

  [Header("Attack Stats")]
  public float attackDamage = 10f;
  public float attackDuration = 1f;
}