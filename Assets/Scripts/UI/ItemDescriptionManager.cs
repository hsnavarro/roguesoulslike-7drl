using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionManager : MonoBehaviour {

  [SerializeField]
  public Item item;
  [SerializeField]
  public Transform itemUIPivot;

  private string itemName;
  private string itemDescription;
  private Color color;

  private Color white;
  private Color blue;
  private Color purple;
  private Color orange;

  [SerializeField]
  public Text itemTextUI;

  public void FormName() {
    string rarity = "Default";
    switch (item.rarity) {
      case Rarity.COMMON:
        rarity = "Common";
        itemTextUI.color = white; 
        break;
      
      case Rarity.EPIC:
        rarity = "Epic";
        itemTextUI.color = purple; 
        break;

      case Rarity.RARE:
        rarity = "Rare";
        itemTextUI.color = blue; 
        break;

      case Rarity.LEGENDARY:
        rarity = "Legendary";
        itemTextUI.color = orange;
        break;

      default:
        break;

    }

    itemName = item.baseName + " (" + rarity + ")";
  }

  public void FormDescription() {
    string description = "";

    if (item.attackDamageMultiplierIncrease > 0f) {
      description += "Attack Multiplier +" + Mathf.FloorToInt(item.attackDamageMultiplierIncrease) + "%\n";
    }

    if (item.healthIncrease > 0f) {
      description += "Health +" + Mathf.FloorToInt(item.healthIncrease) + "\n";
    }

    if (item.staminaIncrease > 0f) {
      description += "Stamina +" + Mathf.FloorToInt(item.staminaIncrease) + "\n";
    }

    if (item.staminaRechargeDelayDecrease > 0f) {
      description += "Stamina Recharge Time -" + string.Format("{0:N2}", item.staminaRechargeDelayDecrease) + "\n";
    }

    if (item.staminaRechargeRateIncrease > 0f) {
      description += "Stamina Recharge Rate +" + Mathf.FloorToInt(item.staminaRechargeRateIncrease) + "\n";
    }

    if (item.flasksCapacityIncrease > 0f) {
      description += "Flask Capacity +" + Mathf.FloorToInt(item.flasksCapacityIncrease) + "\n";
    }

    if (item.flaskHealthRegenerationIncrease > 0f) {
      description += "Flask Health Regeneration +" + Mathf.FloorToInt(item.flaskHealthRegenerationIncrease) + "\n";
    }

    itemDescription = description;
  } 

  private void Start() {
    // RGB 0-1.0
    white = new Color(1f, 1f, 1f, 1f);
    blue = new Color(0f, 0f, 1f, 1f);
    purple = new Color(0.4862745f, 0f, 1f, 1f);
    orange = new Color(1f, 0.6117647f, 0f, 1f);

    FormName();
    FormDescription();
  }

  public void Hide() {
    itemTextUI.text = "";
  }

  public void Show() {
    itemTextUI.text = itemName + "\n" + itemDescription;
  }

  private void Update() {
    //itemUIPivot.LookAt(Camera.main.transform.position);
  }
}
