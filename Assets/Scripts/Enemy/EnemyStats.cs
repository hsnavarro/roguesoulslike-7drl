using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
  public EnemyTypes type;
  public Defense defense;

  public float barIncreaseAfterDeath = 1f;
  public float speed = 5f;

  public float attackDamage = 10f;
  public float attackDuration = 1f;
}