using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private bool isEquipped;

    [SerializeField]
    public string baseName;
    [SerializeField]
    public string modifierName;

    [SerializeField]
    public Rarity rarity;

    private PlayerStats playerStats;

    [SerializeField]
    public float attackDamageMultiplierIncrease;

    [SerializeField]
    public float healthIncrease;

    [SerializeField]
    public float staminaIncrease;
    [SerializeField]    
    public float staminaRechargeDelayDecrease;
    [SerializeField]
    public float staminaRechargeRateIncrease;

    [SerializeField]
    public int flasksCapacityIncrease;
    [SerializeField]
    public float flaskHealthRegenerationIncrease;

    private void UpdateStats(int multiplier) {
        playerStats.attackMultiplier += multiplier * attackDamageMultiplierIncrease;
        
        playerStats.defense.maxHealth += multiplier * healthIncrease;

        playerStats.maxStamina += multiplier * staminaIncrease;
        playerStats.staminaRechargeDelay -= multiplier * staminaRechargeDelayDecrease;
        playerStats.staminaRechargeRate += multiplier * staminaRechargeRateIncrease;

        playerStats.flasksCapacity += multiplier * flasksCapacityIncrease;
        playerStats.flaskHealthIncrease += multiplier * flaskHealthRegenerationIncrease;
    }

    public void AddEffects() {
        if(isEquipped) {
            Debug.Log("Item can't be equipped twice");
            return;
        }
        isEquipped = true;
        UpdateStats(1);
    }

    public void RemoveEffects() {
        if(!isEquipped) {
            Debug.Log("Item can't be unequipped twice");
            return;           
        }
        isEquipped = false;
        UpdateStats(-1);
    }

    private void Start() {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }


}
