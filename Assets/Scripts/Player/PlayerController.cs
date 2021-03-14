using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
  /*
  [Header("Control Canvas")]
  [SerializeField]
  private Canvas controlsCanvas;
  */

  [Header("Death Scene Canvas")]
  [SerializeField]
  private DeathCanvas deathSceneCanvas;
  [Header("Scene to load when Dead")]
  [SerializeField]
  private string sceneName = "Naum";

  [Header("Player References")]
  [SerializeField]
  private PlayerInput playerInput;
  [SerializeField]
  private PlayerItemUsage playerItemUsage;
  [SerializeField]
  private PlayerMovement playerMovement;
  [SerializeField]
  private PlayerStats playerStats;
  [SerializeField]
  private PlayerSkillTree playerSkillTree;
  [SerializeField]
  private PlayerWeapon playerWeapon;
  [SerializeField]
  private Animator playerAnimator;

  //private bool isControlsUIActive = false;

  private float deathTimeInputDelay = 2f;
  private bool canDeathTimeReceiveInput = false;

  // Attack variables. Maybe move to a better place
  private bool isLightAttacking = false;
  private bool isHeavyAttacking = false;
  private int actingCount = 0;

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

  public bool IsActing {
    get { return actingCount > 0; }
  }

  public void OnFlaskUse(InputAction.CallbackContext context) {
    if(playerStats.playerResilience.currentHealth == playerStats.playerResilience.maxHealth) return;

    if(context.started) {
      playerItemUsage.UseFlask();
    }
  }

  public void OnSeeProgress(InputAction.CallbackContext context) {
    /*
    if(context.started) {
      isControlsUIActive ^= true;

      if(isControlsUIActive) {
        foreach(var action in playerInput.actions) if(action.name != "See Controls") action.Disable();
      } else {
        playerInput.currentActionMap.Enable();
      }

      controlsCanvas.gameObject.SetActive(isControlsUIActive);
    }
    */
  }

  public void OnInteractSlot1(InputAction.CallbackContext context) {
    if (context.started) playerItemUsage.HandleInteractSlot(0);
  }

  public void OnInteractSlot2(InputAction.CallbackContext context) {
    if (context.started) playerItemUsage.HandleInteractSlot(1);
  }

  public void OnInteractSlot3(InputAction.CallbackContext context) {
    if (context.started) playerItemUsage.HandleInteractSlot(2);
  }

  public void OnInteractSlot4(InputAction.CallbackContext context) {
    if (context.started) playerItemUsage.HandleInteractSlot(3);
  }

  public void OnDash(InputAction.CallbackContext context) {
    if(playerStats.currentStamina < playerStats.dashStaminaDecrease) return;
    if (IsActing || playerMovement.isDashing || playerMovement.playerDirection == Vector3.zero)
        return;

    if (context.started) {
      playerAnimator.SetBool("Dashing", true);
    }
  }

  public void OnMove(InputAction.CallbackContext context) {
    Vector2 input = context.ReadValue<Vector2>().normalized;

    playerMovement.playerDirection = new Vector3(input.x, 0, input.y);
    playerMovement.playerDirection = Quaternion.Euler(0, -45, 0) * playerMovement.playerDirection;    
  }

  public void OnHeavyAttack(InputAction.CallbackContext context) {
    if (IsActing) return;

    if (context.started) {
      // playerAnimator.SetTrigger("HeavyAttack");
    }
  }

  public void OnLightAttack(InputAction.CallbackContext context) {
    if (playerStats.currentStamina < playerStats.lightAttackStaminaDecrease) return;
    if (IsActing) return;

    if (context.started) {
      playerAnimator.SetTrigger("LightAttack");
    }

  }

  public void DeathSceneOnInput(InputAction.CallbackContext context) {
    if (context.started && canDeathTimeReceiveInput) {
      SceneManager.LoadScene(sceneName);
    }
  }

  public void OnRun(InputAction.CallbackContext context) {
    if (context.canceled) playerMovement.isRunButtonActive = false;
    else if (context.performed) playerMovement.isRunButtonActive = true;
  }

  public void OnDeath() {
    playerSkillTree.SetPermanentInformation();
    StartCoroutine(OnDeathCoroutine());
  }

  IEnumerator OnDeathCoroutine() {
    playerInput.SwitchCurrentActionMap("DeathUI");
    canDeathTimeReceiveInput = false;
    deathSceneCanvas.Show(deathTimeInputDelay);

    yield return new WaitForSeconds(deathTimeInputDelay);

    deathSceneCanvas.ShowContinue();
    canDeathTimeReceiveInput = true;
  }
}