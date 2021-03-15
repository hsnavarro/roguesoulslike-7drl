using UnityEngine;

public class Item : MonoBehaviour {
    [Header("Item Name")]
    public string baseName;
    public string modifierName;

    [Header("Rarity")]
    public Rarity rarity;

    [Header("Attack Effects")]
    public float attackDamageMultiplierIncrease;

    [Header("Health Effects")]
    public float healthIncrease;

    [Header("Stamina Effects")]
    public float staminaIncrease;
    public float staminaRechargeDelayDecrease;
    public float staminaRechargeRateIncrease;

    [Header("Flask Effects")]
    public int flasksCapacityIncrease;
    public float flaskHealthRegenerationIncrease;

    private bool isEquipped;

    private PlayerStats playerStats;

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

    private void UpdateStats(int multiplier) {
        playerStats.attackMultiplier += multiplier * attackDamageMultiplierIncrease;
        
        playerStats.playerResilience.maxHealth += multiplier * healthIncrease;

        playerStats.maxStamina += multiplier * staminaIncrease;
        playerStats.staminaRechargeDelay -= multiplier * staminaRechargeDelayDecrease;
        playerStats.staminaRechargeRate += multiplier * staminaRechargeRateIncrease;

        playerStats.flasksCapacity += multiplier * flasksCapacityIncrease;
        playerStats.flaskHeal += multiplier * flaskHealthRegenerationIncrease;
    }
}
