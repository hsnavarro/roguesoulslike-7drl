using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ItemGeneration : MonoBehaviour {   
    private List<Item> baseItems;
    public List<GameObject> itemsGenerated;

    private Vector3 offset;
    
    [SerializeField]
    public GameObject legendaryItemPrefab;
    public GameObject epicItemPrefab;
    public GameObject rareItemPrefab;
    public GameObject commonItemPrefab;

    public GameObject InstantiateItem(Rarity itemRarity) {
        GameObject itemGameObject;
        switch (itemRarity) {
            case Rarity.COMMON:
                itemGameObject = Instantiate(commonItemPrefab, Vector3.zero, Quaternion.identity);
                itemGameObject.SetActive(true);
                return itemGameObject;
            case Rarity.RARE:
                itemGameObject = Instantiate(rareItemPrefab, Vector3.zero, Quaternion.identity);
                itemGameObject.SetActive(true);
                return itemGameObject;
            case Rarity.EPIC:
                itemGameObject = Instantiate(epicItemPrefab, Vector3.zero, Quaternion.identity);
                itemGameObject.SetActive(true);
                return itemGameObject;
            case Rarity.LEGENDARY:
                itemGameObject = Instantiate(legendaryItemPrefab, Vector3.zero, Quaternion.identity);
                itemGameObject.SetActive(true);
                return itemGameObject;
            default:
            // Idk what should we do in this case
                itemGameObject = Instantiate(commonItemPrefab, Vector3.zero, Quaternion.identity);
                itemGameObject.SetActive(true);
                return itemGameObject;
        }
    }

    public void GenerateItems(int numberOfRareItems, int numberOfEpicItems) {
        GenerateCommon();
        GenerateRare(numberOfRareItems);
        GenerateEpic(numberOfEpicItems);
        GenerateLegendary();
    }

    private void Start() {
        baseItems = new List<Item>();
    }

    private void GenerateCommon() {
        GameObject attackGameObject = InstantiateItem(Rarity.COMMON);
        Item attackItem = attackGameObject.GetComponentInChildren<Item>();

        attackGameObject.name = "Common Item 1";
        attackItem.baseName = "Attack Item";
        attackItem.modifierName = "Furious";
        attackItem.attackDamageMultiplierIncrease = 10f;

        baseItems.Add(attackItem);
        itemsGenerated.Add(attackGameObject);

        GameObject healthIncreaseGameObject = InstantiateItem(Rarity.COMMON);
        Item healthItem = healthIncreaseGameObject.GetComponentInChildren<Item>();

        healthIncreaseGameObject.name = "Common Item 2";
        healthItem.baseName = "Health Item";
        healthItem.modifierName = "Strong";
        healthItem.healthIncrease = 10f;

        baseItems.Add(healthItem);
        itemsGenerated.Add(healthIncreaseGameObject);

        GameObject staminaGameObject = InstantiateItem(Rarity.COMMON);
        Item staminaItem = staminaGameObject.GetComponentInChildren<Item>();

        staminaGameObject.name = "Common Item 3";
        staminaItem.baseName = "Stamina Item";
        staminaItem.modifierName = "Fitness";
        staminaItem.staminaIncrease = 10f;

        baseItems.Add(staminaItem);
        itemsGenerated.Add(staminaGameObject);

        GameObject staminaDelayGameObject = InstantiateItem(Rarity.COMMON);
        Item staminaDelayItem = staminaDelayGameObject.GetComponentInChildren<Item>();

        staminaDelayGameObject.name = "Common Item 4";
        staminaDelayItem.baseName = "Stamina Delay Item";
        staminaDelayItem.modifierName = "Impatient";
        staminaDelayItem.staminaRechargeDelayDecrease = 0.5f;

        baseItems.Add(staminaDelayItem); 
        itemsGenerated.Add(staminaDelayGameObject);

        GameObject staminaRechargeGameObject = InstantiateItem(Rarity.COMMON);
        Item staminaRechargeItem = staminaRechargeGameObject.GetComponentInChildren<Item>();

        staminaRechargeGameObject.name = "Common Item 5";
        staminaRechargeItem.baseName = "Stamina Recharge Item";
        staminaRechargeItem.modifierName = "Fast";
        staminaRechargeItem.staminaRechargeRateIncrease = 10f;

        baseItems.Add(staminaRechargeItem);
        itemsGenerated.Add(staminaRechargeGameObject);

        GameObject flaskCapacityGameObject = InstantiateItem(Rarity.COMMON);
        Item flaskCapacityItem = flaskCapacityGameObject.GetComponentInChildren<Item>();

        flaskCapacityGameObject.name = "Common Item 6";
        flaskCapacityItem.baseName = "Flask Capacity Item";
        flaskCapacityItem.modifierName = "Scavenger";
        flaskCapacityItem.flasksCapacityIncrease = 1;

        baseItems.Add(flaskCapacityItem);
        itemsGenerated.Add(flaskCapacityGameObject);

        GameObject flaskRegenerationGameObject = InstantiateItem(Rarity.COMMON);
        Item flaskRegenerationItem = flaskRegenerationGameObject.GetComponentInChildren<Item>();

        flaskRegenerationGameObject.name = "Common Item 7";
        flaskRegenerationItem.baseName = "Flask Regeneration Item";
        flaskRegenerationItem.modifierName = "Specialist";
        flaskRegenerationItem.flaskHealthRegenerationIncrease = 10f;

        baseItems.Add(flaskRegenerationItem);
        itemsGenerated.Add(flaskRegenerationGameObject);
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


    private void GenerateCombined(int numberOfItemsCombined, int numberOfItemsToGenerate, Rarity itemRarity) {
        int generateCounter = 1;

        GameObject itemGameObject;
        Item item;

        Item itemChosen;

        while(generateCounter <= numberOfItemsToGenerate) {
            itemGameObject = InstantiateItem(itemRarity);
            item = itemGameObject.GetComponentInChildren<Item>();

            if (numberOfItemsCombined == 2) {
                item.rarity = Rarity.RARE;
                itemGameObject.name = "Rare Item " + generateCounter.ToString();
            }

            if (numberOfItemsCombined == 3) {
                item.rarity = Rarity.EPIC;
                itemGameObject.name = "Epic Item " + generateCounter.ToString();
            }

            for(int i = 0; i < numberOfItemsCombined; i++) {
                
                int randomIndex = Random.Range(0, baseItems.Count);
                itemChosen = baseItems[randomIndex];
                CopyEffects(item, itemChosen);

                if (i == numberOfItemsCombined - 1) {
                    item.baseName += itemChosen.baseName;
                } else {
                    item.baseName += itemChosen.modifierName + " ";
                }
            }

            itemsGenerated.Add(itemGameObject);

            generateCounter++;
        }
    }

    private void GenerateRare(int numberOfRareItems) {
        GenerateCombined(2, numberOfRareItems, Rarity.RARE);
    }   

    private void GenerateEpic(int numberOfEpicItems) {
        GenerateCombined(3, numberOfEpicItems, Rarity.EPIC);
    }

    private void GenerateLegendary() {
        GameObject legendaryGameObject = InstantiateItem(Rarity.LEGENDARY);
        Item legendaryItem = legendaryGameObject.GetComponentInChildren<Item>();

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

        itemsGenerated.Add(legendaryGameObject);
    }
}
