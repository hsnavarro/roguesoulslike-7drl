using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

  public PlayerStats playerStats;
  public EnemyStats enemyStats;
  public CharacterController enemyController;

  private void OnControllerColliderHit(ControllerColliderHit hit) {
    //Debug.print("Enemy collided with " + hit.gameObject.name);
    if (hit.gameObject.name == "Player") {
      HP playerHP = hit.gameObject.GetComponent<HP>();
      playerHP.TakeDamage(enemyStats.damage);
    }
  }
}