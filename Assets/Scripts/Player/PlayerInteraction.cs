using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour {
  public PlayerStats playerStats;
  public PlayerItemInteraction playerItemInteraction;
  public PlayerSkillTree playerSkillTree;
  public PlayerInput playerInput;

  private GameObject deathSceneCanvas;
  public string sceneName = "Naum";

  void Start() {
    deathSceneCanvas = GameObject.FindGameObjectWithTag("DeathSceneCanvas");
  }

  private void OnTriggerStay(Collider collider) {
    if(collider.isTrigger) return;

    if (collider.gameObject.layer == (int)Layers.ENEMY) {
      Defense enemyDefense = collider.gameObject.GetComponent<Defense>();
      enemyDefense.TakeDamage(playerStats.currentDamage);
    }
  }
  private void OnControllerColliderHit(ControllerColliderHit hit) {

    // Edible Item 
    if (hit.gameObject.layer == (int)Layers.EDIBLE_ITEM) {
      EdibleItem item = hit.gameObject.GetComponent<EdibleItem>();
      playerItemInteraction.Use(item);
      hit.gameObject.SetActive(false);
    }
  }

  private void Awake() {
    playerSkillTree.GetPermanentInformation();
    playerStats.defense.maxHealth += PermanentPlayerInformation.healthIncrease;
    playerStats.flasksCapacity += PermanentPlayerInformation.flasksCapacityIncrease;
    playerStats.maxStamina += PermanentPlayerInformation.staminaIncrease;
  }

  private void Update() {
    if(playerStats.defense.IsDead()) {
      playerSkillTree.SetPermanentInformation();
      deathSceneCanvas.SetActive(true);
      playerInput.SwitchCurrentActionMap("DeathUI");
    }
  }

  public void DeathSceneOnInput(InputAction.CallbackContext context) {
    if (context.started) {
        SceneManager.LoadScene(sceneName);
    }
  }
}