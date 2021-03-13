using UnityEngine;

public class EnemyStats : MonoBehaviour {
  public enum RewardType {
    Health,
    Stamina,
    Flask
  }

  public Resilience enemyResilience;

  [Header("Player Progress Bar Stats")]
  public float rewardAmount = 1f;
  public RewardType reward;
  
  [Header("Speed Stats")]
  public float speed = 5f;

  [Header("Attack Stats")]
  public float attackDamage = 10f;
  public float attackDelay = 1f; 
}