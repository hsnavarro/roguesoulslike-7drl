using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    public void Heal(float value)
    {
        currentHP = Mathf.Min(maxHP, currentHP + value);
    } 

    public void TakeDamage(float value)
    {
        currentHP = Mathf.Max(0f, currentHP - value);
    }

    public void IncreaseHP(float value)
    {
        maxHP += value;
    }

    public void DecreaseHP(float value)
    {
        maxHP = Mathf.Max(0f, maxHP - value);
    }
}
