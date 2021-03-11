using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerCharacterController : MonoBehaviour {

  [SerializeField]
  private PlayerItemInteraction itemInteraction;

  [SerializeField]
  private PlayerMovement movement;
  [SerializeField]
  private PlayerStats stats;

  public PlayerInput playerInput;
  bool isSkillTreeUIActive = false;
  public Canvas skillTreeCanvas;

  public void OnSeeProgress(InputAction.CallbackContext context) {
    if(context.started) {
      isSkillTreeUIActive ^= true;

      if(isSkillTreeUIActive) {
        foreach(var action in playerInput.actions) if(action.name != "See Progress") action.Disable();
      } else {
        playerInput.currentActionMap.Enable();
      }

      skillTreeCanvas.gameObject.SetActive(isSkillTreeUIActive);
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
    if (context.started) {
      movement.dashDirection = movement.playerDirection;
      if (movement.isDashing || movement.playerDirection == Vector3.zero) return;
      movement.dashTimer = 0f;
      movement.isDashing = true;
    }
  }

  public void OnMove(InputAction.CallbackContext context) {
    Vector2 input = context.ReadValue<Vector2>().normalized;

    movement.playerDirection = new Vector3(input.x, 0, input.y);
    movement.playerDirection = Quaternion.Euler(0, -45, 0) * movement.playerDirection;    
  }
  public void OnHeavyAttack(InputAction.CallbackContext context) {
    if(stats.isHeavyAttacking || stats.isLightAttacking) return;

    if (context.started) {
      stats.isHeavyAttacking = true;
      stats.heavyAttackTimer = 0f;
      stats.currentStamina = Mathf.Max(0f, stats.currentStamina - stats.heavyAttackStaminaDecrease);
    }
  }

  public void OnLightAttack(InputAction.CallbackContext context) {
    if(stats.isHeavyAttacking || stats.isLightAttacking) return;

    if (context.started) {
      stats.isLightAttacking = true;
      stats.lightAttackTimer = 0f;
      stats.currentStamina = Mathf.Max(0f, stats.currentStamina - stats.lightAttackStaminaDecrease);
    }
  }

  public void OnRun(InputAction.CallbackContext context) {
    if (context.canceled) movement.isRunButtonActive = false;
    else if (context.performed) movement.isRunButtonActive = true;
  }
}
