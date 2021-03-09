using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerCharacterController : MonoBehaviour {

  private enum Layers { PLAYER = 8, PLAYER_DASHING = 10 };

  [SerializeField]
  private CharacterController playerController;
  [SerializeField]
  private PlayerStats playerStats;

  private float staminaDelayTimer = 0f;

  private Vector3 playerDirection = Vector3.zero;
  private Vector3 playerVelocity = Vector3.zero;
  private bool isRunButtonActive = false;

  private Animator anim;

  private bool isDashing = false;
  private Vector3 dashDirection = Vector3.zero;
  private float dashTimer = 0f;

  private void updateLayer() {
    if (isDashing) gameObject.layer = (int)Layers.PLAYER_DASHING;
    else gameObject.layer = (int)Layers.PLAYER;

  }
  public void OnDash(InputAction.CallbackContext context) {
    if (context.started) {
      dashDirection = playerDirection;
      if (isDashing || playerDirection == Vector3.zero) return;
      dashTimer = 0f;
      isDashing = true;
    }
  }

  public void handleDash() {
    if (isDashing) {
      dashTimer += Time.deltaTime;
      if (dashTimer > playerStats.dashDuration) isDashing = false;
    }
  }


  private void Awake() {
    anim = GetComponent<Animator>();
  }

  public void OnMove(InputAction.CallbackContext context) {
    Vector2 input = context.ReadValue<Vector2>();
    playerDirection = new Vector3(input.x, 0, input.y);
  }

  private void ApplyMovement() {
    handleDash();
    updateLayer();

    playerStats.isDashing = isDashing;

    if (isDashing) playerVelocity = dashDirection * playerStats.dashSpeed;
    else playerVelocity = playerDirection * playerStats.speed;

    playerController.Move(playerVelocity * Time.deltaTime);
  }

  public void OnHeavyAttack(InputAction.CallbackContext context) {
    if (context.started) {
      playerStats.damage = playerStats.heavyAttackDamage;
      playerStats.Stamina = Mathf.Max(0f, playerStats.Stamina - playerStats.heavyAttackStaminaDecrease);
    }
  }

  public void OnLightAttack(InputAction.CallbackContext context) {
    if (context.started) {
      playerStats.damage = playerStats.lightAttackDamage;
      playerStats.Stamina = Mathf.Max(0f, playerStats.Stamina - playerStats.lightAttackStaminaDecrease);
    }
  }

  public void OnRun(InputAction.CallbackContext context) {
    if (context.canceled) isRunButtonActive = false;
    else if (context.performed) isRunButtonActive = true;
  }

  private void HandleRun() {
    bool isRunning = isRunButtonActive && playerStats.Stamina > 0f;
    bool isMoving = playerDirection != Vector3.zero;

    if (isRunning) {
      playerStats.speed = playerStats.runSpeed;

      if (isMoving) {
        playerStats.Stamina = Mathf.Max(0f,
            playerStats.Stamina - Time.deltaTime * playerStats.staminaRunDecreaseRate);

        staminaDelayTimer = 0f;
      }
    } else {
      playerStats.speed = playerStats.normalSpeed;
    }

    if (playerStats.Stamina != playerStats.maxStamina) staminaDelayTimer += Time.deltaTime;

    if ((!isRunning || !isMoving) && staminaDelayTimer >= playerStats.staminaRechargeDelay) {
      playerStats.Stamina = Mathf.Min(playerStats.maxStamina,
           playerStats.Stamina + Time.deltaTime * playerStats.staminaRechargeRate);
    }

    // Rotation update
    Vector3 targetPosition = new Vector3(playerDirection.x, 0f, playerDirection.z);
    targetPosition += transform.position;
    transform.LookAt(targetPosition);

    // Animation update
    anim.SetInteger("MoveState", isMoving ? (isRunning ? 2 : 1) : 0);
  }

  private void DebugInfo() {
    Debug.print("Player velocity vector " + playerVelocity);
  }

  // Update is called once per frame
  void Update() {
    HandleRun();
    ApplyMovement();

    //DebugInfo();
  }
}
