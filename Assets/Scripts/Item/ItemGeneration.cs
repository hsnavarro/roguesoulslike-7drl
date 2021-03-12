using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : MonoBehaviour
{   
    [SerializeField]
    private List<Item> baseItems;

    private Vector3 offset;
    
    [SerializeField]
    private GameObject itemGameObjectPrefab;

    [SerializeField]
    private const int numberOfCommonItems = 7;
    [SerializeField]
    private int numberOfRareItems = 5;
    [SerializeField]
    private int numberOfEpicItems = 3;
    [SerializeField]
    private const int numberOfLegendaryItems = 1;

    private GameObject InstantiateItem() {
        Vector3 randomUpwardsVector = new Vector3(Random.Range(-1f, 1f), Random.value, Random.Range(-1f, 1f));
        randomUpwardsVector = randomUpwardsVector.normalized;

        GameObject itemGameObject =  Instantiate(itemGameObjectPrefab, Vector3.zero + Random.Range(3f, 20f) * randomUpwardsVector, Quaternion.identity);
        itemGameObject.SetActive(true);
        return itemGameObject;
    }

    private void GenerateCommon() {
        GameObject attackGameObject = InstantiateItem();
        Item attackItem = attackGameObject.GetComponent<Item>();

        attackGameObject.name = "Common Item 1";
        attackItem.baseName = "Attack Item";
        attackItem.modifierName = "Furious";
        attackItem.attackDamageMultiplierIncrease = 10f;

        baseItems.Add(attackItem);

        GameObject healthIncreaseGameObject = InstantiateItem();
        Item healthItem = healthIncreaseGameObject.GetComponent<Item>();

        healthIncreaseGameObject.name = "Common Item 2";
        healthItem.baseName = "Health Item";
        healthItem.modifierName = "Strong";
        healthItem.healthIncrease = 10f;

        baseItems.Add(healthItem);

        GameObject staminaGameObject = InstantiateItem();
        Item staminaItem = staminaGameObject.GetComponent<Item>();

        staminaGameObject.name = "Common Item 3";
        staminaItem.baseName = "Stamina Item";
        staminaItem.modifierName = "Fitness";
        staminaItem.staminaIncrease = 10f;

        baseItems.Add(staminaItem);

        GameObject staminaDelayGameObject = InstantiateItem();
        Item staminaDelayItem = staminaDelayGameObject.GetComponent<Item>();

        staminaDelayGameObject.name = "Common Item 4";
        staminaDelayItem.baseName = "Stamina Delay Item";
        staminaDelayItem.modifierName = "Impatient";
        staminaDelayItem.staminaRechargeDelayDecrease = 10f;

        baseItems.Add(staminaDelayItem); 

        GameObject staminaRechargeGameObject = InstantiateItem();
        Item staminaRechargeItem = staminaRechargeGameObject.GetComponent<Item>();

        staminaRechargeGameObject.name = "Common Item 5";
        staminaRechargeItem.baseName = "Stamina Recharge Item";
        staminaRechargeItem.modifierName = "Fast";
        staminaRechargeItem.staminaRechargeRateIncrease = 10f;

        baseItems.Add(staminaRechargeItem);

        GameObject flaskCapacityGameObject = InstantiateItem();
        Item flaskCapacityItem = flaskCapacityGameObject.GetComponent<Item>();

        flaskCapacityGameObject.name = "Common Item 6";
        flaskCapacityItem.baseName = "Flask Capacity Item";
        flaskCapacityItem.modifierName = "Scavenger";
        flaskCapacityItem.flasksCapacityIncrease = 1;

        baseItems.Add(flaskCapacityItem);

        GameObject flaskRegenerationGameObject = InstantiateItem();
        Item flaskRegenerationItem = flaskRegenerationGameObject.GetComponent<Item>();

        flaskRegenerationGameObject.name = "Common Item 7";
        flaskRegenerationItem.baseName = "Flask Regeneration Item";
        flaskRegenerationItem.modifierName = "Specialist";
        flaskRegenerationItem.flaskHealthRegenerationIncrease = 10f;

        baseItems.Add(flaskRegenerationItem);
    }

    private void CopyEffects(Item setItem, Item getItem) {
        setItem.attackDamageMultiplierIncrease += getItem.attackDamageMultiplierIncrease;

        setItem.healthIncrease += getItem.healthIncrease;

        setItem.staminaIncrease += getItem.staminaIncrease;
        setItem.staminaRechargeDelayDecrease += getItem.staminaRechargeDelayDecrease;
        setItem.staminaRechargeRateIncrease += getItem.staminaRechargeRateIncrease;

        setItem.flasksCapacityIncrease += getItem.flasksCapacityIncrease;
        setItem.flaskHealthRegenerationIncrease += getItem.flaskHealthRegenerationIncrease;
    }


    private void GenerateCombined(int numberOfItemsCombined, int numberOfItemsToGenerate) {

        int generateCounter = 1;

        GameObject itemGameObject;
        Item item;

        Item itemChosen;

        System.Random rand = new System.Random();

        while(generateCounter <= numberOfItemsToGenerate) {
            itemGameObject = InstantiateItem();
            item = itemGameObject.GetComponent<Item>();

            if (numberOfItemsCombined == 2) {
                item.rarity = Rarity.RARE;
                itemGameObject.name = "Rare Item " + generateCounter.ToString();
            }

            if (numberOfItemsCombined == 3) {
                item.rarity = Rarity.EPIC;
                itemGameObject.name = "Epic Item " + generateCounter.ToString();
            }

            for(int i = 0; i < numberOfItemsCombined; i++) {
                
                int randomIndex = rand.Next(0, baseItems.Count);
                itemChosen = baseItems[randomIndex];
                CopyEffects(item, itemChosen);

                if (i == numberOfItemsCombined - 1) {
                    item.baseName += itemChosen.baseName;
                } else {
                    item.baseName += itemChosen.modifierName + " ";
                }
            }

            generateCounter++;
        }

    }

    private void GenerateRare() {
        GenerateCombined(2, numberOfRareItems);
    }   

    private void GenerateEpic() {
        GenerateCombined(3, numberOfEpicItems);
    }

    private void GenerateLegendary() {
        GameObject legendaryGameObject = InstantiateItem();
        Item legendaryItem = legendaryGameObject.GetComponent<Item>();

        legendaryGameObject.name = "Legendary Item";
        legendaryItem.rarity = Rarity.LEGENDARY;
        legendaryItem.baseName = "Legendary Item";

        legendaryItem.attackDamageMultiplierIncrease = 10f;

        legendaryItem.healthIncrease = 10f;

        legendaryItem.staminaIncrease = 10f;
        legendaryItem.staminaRechargeDelayDecrease = 0.5f;
        legendaryItem.staminaRechargeRateIncrease = 10f;

        legendaryItem.flasksCapacityIncrease = 1;
        legendaryItem.flaskHealthRegenerationIncrease = 10f;
    }


    
    public void Start()
    {
        offset = new Vector3(4f, 1f, -4f);
        GenerateCommon();
        GenerateRare();
        GenerateEpic();
        GenerateLegendary();
    }
}
