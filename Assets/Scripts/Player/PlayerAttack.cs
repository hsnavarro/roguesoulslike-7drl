using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  public PlayerStats playerStats;
  public CharacterController playerController;

  private void OnControllerColliderHit(ControllerColliderHit hit) {
    //Debug.print("Player collided with " + hit.gameObject.name);
    if (hit.gameObject.tag == "Enemy") {
      HP enemyHP = hit.gameObject.GetComponent<HP>();
      enemyHP.TakeDamage(playerStats.damage);
    }
  }
}