using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControllerEvents : MonoBehaviour {
  public GameObject deathSceneCanvas;
  public string sceneName = "Naum";

  [SerializeField]
  private PlayerItemInteraction itemInteraction;

  [SerializeField]
  private PlayerMovement movement;

  [SerializeField]
  private PlayerStats stats;
   [SerializeField]
  private PlayerSkillTree playerSkillTree;

  [SerializeField]
  private Animator anim;

  public PlayerInput playerInput;
  bool isControlsUIActive = false;
  public Canvas controlsCanvas;

  private float deathTime;
  private float deathTimeInputDelay = 2f;

  public void OnFlaskUse(InputAction.CallbackContext context) {
    if(stats.defense.currentHealth == stats.defense.maxHealth) return;

    if(context.started) {
      itemInteraction.UseFlask();
    }
  }

  public void OnSeeProgress(InputAction.CallbackContext context) {
    if(context.started) {
      isControlsUIActive ^= true;

      if(isControlsUIActive) {
        foreach(var action in playerInput.actions) if(action.name != "See Controls") action.Disable();
      } else {
        playerInput.currentActionMap.Enable();
      }

      controlsCanvas.gameObject.SetActive(isControlsUIActive);
    }
  }

  public void OnInteractSlot1(InputAction.CallbackContext context) {
    if (context.started) itemInteraction.HandleInteractSlot(0);
  }

  public void OnInteractSlot2(InputAction.CallbackContext context) {
    if (context.started) itemInteraction.HandleInteractSlot(1);
  }

  public void OnInteractSlot3(InputAction.CallbackContext context) {
    if (context.started) itemInteraction.HandleInteractSlot(2);
  }

  public void OnInteractSlot4(InputAction.CallbackContext context) {
    if (context.started) itemInteraction.HandleInteractSlot(3);
  }
  public void OnDash(InputAction.CallbackContext context) {
    if(stats.currentStamina == 0f) return;
    if (movement.isDashing || movement.playerDirection == Vector3.zero) return;

    if (context.started) {
      movement.dashDirection = movement.playerDirection;
      movement.dashTimer = 0f;
      movement.isDashing = true;
      movement.DecreaseStamina(stats.dashStaminaDecrease);
      movement.RestartStaminaRechargeTimer();
    }
  }

  public void OnMove(InputAction.CallbackContext context) {
    Vector2 input = context.ReadValue<Vector2>().normalized;

    movement.playerDirection = new Vector3(input.x, 0, input.y);
    movement.playerDirection = Quaternion.Euler(0, -45, 0) * movement.playerDirection;    
  }
  public void OnHeavyAttack(InputAction.CallbackContext context) {
    if(stats.currentStamina == 0f) return;
    if(stats.isHeavyAttacking || stats.isLightAttacking) return;

    if (context.started) {
      stats.isHeavyAttacking = true;
      stats.heavyAttackTimer = 0f;
      movement.DecreaseStamina(stats.heavyAttackStaminaDecrease);
      movement.RestartStaminaRechargeTimer();
    }
  }

  public void OnLightAttack(InputAction.CallbackContext context) {
    if(stats.currentStamina == 0f) return;
    if(stats.isHeavyAttacking || stats.isLightAttacking) return;

    if (context.started) {
      stats.isLightAttacking = true;
      stats.lightAttackTimer = 0f;
      movement.DecreaseStamina(stats.lightAttackStaminaDecrease);
      movement.RestartStaminaRechargeTimer();
    }

    anim.SetTrigger("LightAttack");
  }

  public void DeathSceneOnInput(InputAction.CallbackContext context) {
    if (Time.time - deathTime < deathTimeInputDelay) return;

    if (context.started) {
        SceneManager.LoadScene(sceneName);
    }
  }

  public void OnRun(InputAction.CallbackContext context) {
    if (context.canceled) movement.isRunButtonActive = false;
    else if (context.performed) movement.isRunButtonActive = true;
  }

  public void OnDeath() {
    playerSkillTree.SetPermanentInformation();
    deathSceneCanvas.SetActive(true);
    deathTime = Time.time;
    playerInput.SwitchCurrentActionMap("DeathUI");
  }

}