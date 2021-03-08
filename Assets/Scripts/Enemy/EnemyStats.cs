using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public HP HP;
    public float speed;
    public float damage;
    public void DebugInfo()
    {
        Debug.print("Enemy HP " + HP.currentHP);
    }

    private void Update()
    {
        DebugInfo();
    }
}