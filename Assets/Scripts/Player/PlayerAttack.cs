using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStats playerStats;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.print("Player collided with " + hit.gameObject.name);
        if (hit.gameObject.name == "Enemy")
        {
            HP enemyHP = hit.gameObject.GetComponent<HP>();
            enemyHP.TakeDamage(playerStats.damage);
        }
    }
}