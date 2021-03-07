using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHP = 100f;
    public float maxStamina = 100f;
    public float normalSpeed = 5f;
    public float runSpeed = 10f;

    public float staminaRunDecreaseRate = 15f;
    public float staminaRechargeRate = 20f;
    public float staminaRechargeDelay = 5f;

    public float HP;
    public float Stamina;
    public float speed;

    private void Start()
    {
        HP = maxHP;
        Stamina = maxStamina;
        speed = normalSpeed;
    }
}
