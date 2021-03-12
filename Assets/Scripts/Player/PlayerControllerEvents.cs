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
  private PlayerWeapon playerWeapon;

  [SerializeField]
  private Animator anim;

  public PlayerInput playerInput;
  bool isControlsUIActive = false;
  public Canvas controlsCanvas;

  private float deathTime;
  private float deathTimeInputDelay = 2f;

  // Attack variables. Maybe move to a better place
  private bool isLightAttacking = false;
  public bool IsLightAttacking {
    get { return isLightAttacking; }
    set {
      if (value != isLightAttacking) {
        isLightAttacking = value;

        if (isLightAttacking) {
          actingCount++;
          playerWeapon.StartLightAttack();
        } else {
          actingCount--;
          playerWeapon.EndAttack();
        }
      }
    }
  }

  private bool isHeavyAttacking = false;
  public bool IsHeavyAttacking {
    get { return isHeavyAttacking; }
    set {
      if (value != isHeavyAttacking) {
        isHeavyAttacking = value;

        if (isHeavyAttacking) {
          actingCount++;
          playerWeapon.StartHeavyAttack();
        } else {
          actingCount--;
          playerWeapon.EndAttack();
        }
      }
    }
  }

  private int actingCount = 0;
  public bool IsActing {
    get { return actingCount > 0; }
  }

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
    if (IsActing) return;

    if (context.started) {
      movement.DecreaseStamina(stats.heavyAttackStaminaDecrease);
      movement.RestartStaminaRechargeTimer();
    }
  }

  public void OnLightAttack(InputAction.CallbackContext context) {
    if (IsActing) return;

    if (context.started) {
      stats.currentStamina = Mathf.Max(0f, stats.currentStamina - stats.lightAttackStaminaDecrease);
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