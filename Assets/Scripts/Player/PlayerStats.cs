using UnityEngine;

public class PlayerStats : MonoBehaviour {
  [HideInInspector]
  public const int maxNumberOfItems = 4;
  [HideInInspector]
  public Item[] itemsEquipped;
  [HideInInspector]
  public bool[] isSlotEquipped;

  [Header("Current Stats")]
  public float currentStamina;
  public float currentSpeed;
  public float currentDamage;

  [Header("Resilience Reference")]
  public Resilience playerResilience;

  [Header("Flasks Stats")]
  public int flasksCapacity = 3;
  public int flasksCarried = 0;
  public float flaskShieldIncrease = 0f;
  public float flaskHeal = 30f;

  [Header("Stamina Stats")]
  public float staminaRunDecreaseRate = 15f;
  public float staminaRechargeRate = 20f;
  public float staminaRechargeDelay = 1f;
  public float maxStamina = 100f;

  [Header("Attack Stats")]
  public float attackMultiplier = 100f;

  public float lightAttackStaminaDecrease = 10f;
  public float heavyAttackStaminaDecrease = 30f;
  
  [Header("Speed Stats")]
  public float normalSpeed = 5f;
  public float runSpeed = 10f;

  [Header("Dash Stats")]
  public float dashDuration = 0.5f;
  public float dashSpeed = 20f;
  public float dashStaminaDecrease = 10f;

  public void Start() {
    itemsEquipped = new Item[maxNumberOfItems];
    isSlotEquipped = new bool[maxNumberOfItems];
    currentStamina = maxStamina;
    currentSpeed = normalSpeed;
  }

  public void GetFlask() {
      if (flasksCarried == flasksCapacity) return;
      flasksCarried++;
  }

  private void Update() {
    // Debug.Log("Player Health " + playerResilience.currentHealth);
    // Debug.Log("Player Shield " + defense.currentShield);
    // Debug.Log("Player Stamina " + currentStamina);
    // Debug.Log("Player Damage " + currentDamage * Time.fixedDeltaTime);
  }
}
