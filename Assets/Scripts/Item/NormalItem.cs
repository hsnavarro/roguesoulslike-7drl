using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : MonoBehaviour {

    [SerializeField]
    private int itemID;
    [SerializeField]
    private string typeName;

    [SerializeField]
    private Rarity rarity;

    public PlayerStats playerStats;

    [SerializeField]
    private float heavyAttackDamageIncrease;
    [SerializeField]
    private float lightAttackDamageIncrease;
    [SerializeField]
    private float speedIncrease;
    [SerializeField]
    private float healthIncrease;
    [SerializeField]
    private float staminaIncrease;
    [SerializeField]
    private float shieldIncrease;
    private bool isEquipped;
    private void UpdateStats(int multiplier) {
        playerStats.lightAttackDamage += multiplier * lightAttackDamageIncrease;
        playerStats.heavyAttackDamage += multiplier * heavyAttackDamageIncrease;
        
        playerStats.normalSpeed += multiplier * speedIncrease;
        playerStats.runSpeed += multiplier * speedIncrease;
        
        playerStats.defense.maxHealth += multiplier * healthIncrease;
        playerStats.maxStamina += multiplier * staminaIncrease;
        playerStats.defense.maxShield += multiplier * shieldIncrease;
    }
    public void AddEffects() {
        if(isEquipped) {
            Debug.print("Item can't be equipped twice");
            return;
        }
        isEquipped = true;
        UpdateStats(1);
    }

    public void RemoveEffects() {
        if(!isEquipped) {
            Debug.print("Item can't be unequipped twice");
            return;           
        }
        isEquipped = false;
        UpdateStats(-1);
    }


}
