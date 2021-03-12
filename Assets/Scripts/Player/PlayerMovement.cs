using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  [SerializeField]
  private CharacterController playerController;
  [SerializeField]
  private PlayerStats playerStats;

  private float staminaDelayTimer = 0f;

  public Vector3 playerDirection {get; set; } = Vector3.zero;
  public Vector3 playerVelocity { get; set; }  = Vector3.zero; 
  public bool isRunButtonActive { get; set; } = false;

  public Animator anim;

  public bool isDashing { get; set; }= false;
  public Vector3 dashDirection { get; set; } = Vector3.zero;
  public float dashTimer { get; set; } = 0f;

  private void updateLayer() {
    if (isDashing) gameObject.layer = (int)Layers.PLAYER_DASHING;
    else gameObject.layer = (int)Layers.PLAYER;
  }
  public void handleDash() {
    if (isDashing) {
      dashTimer += Time.deltaTime;
      if (dashTimer > playerStats.dashDuration) isDashing = false;
    }
  }

  private void ApplyMovement() {
    handleDash();
    updateLayer();

    playerStats.isDashing = isDashing;

    if (isDashing) playerVelocity = dashDirection * playerStats.dashSpeed;
    else playerVelocity = playerDirection * playerStats.currentSpeed;

    playerController.Move(playerVelocity * Time.deltaTime);
  }

  public void RestartStaminaRechargeTimer() {
    staminaDelayTimer = 0f;
  }
  private void IncreaseStamina() {
    if (staminaDelayTimer >= playerStats.staminaRechargeDelay) {      
      playerStats.currentStamina = Mathf.Min(playerStats.maxStamina, 
        playerStats.currentStamina + Time.deltaTime * playerStats.staminaRechargeRate);
    }
  }

  public void DecreaseStamina(float value) {
    playerStats.currentStamina = Mathf.Max(0f,
      playerStats.currentStamina - value);
  }

  private void HandleRun() {
    bool isRunning = isRunButtonActive && playerStats.currentStamina > 0f;
    bool isMoving = playerDirection != Vector3.zero;

    if (isRunning) {
      playerStats.currentSpeed = playerStats.runSpeed;

      if (isMoving) {
        DecreaseStamina(Time.deltaTime * playerStats.staminaRunDecreaseRate);
        RestartStaminaRechargeTimer();
      }
    } else {
      playerStats.currentSpeed = playerStats.normalSpeed;
    }


    if (!isRunning || !isMoving) {
      IncreaseStamina();
    }

    // Rotation update
    Vector3 targetPosition = new Vector3(playerDirection.x, 0f, playerDirection.z);
    targetPosition += transform.position;
    transform.LookAt(targetPosition);

    // Animation update
    anim.SetInteger("MoveState", isMoving ? (isRunning ? 2 : 1) : 0);
  }

  private void DebugInfo() {
    Debug.Log("Player velocity vector " + playerVelocity);
  }

  private void Update() {

    if (playerStats.currentStamina != playerStats.maxStamina) staminaDelayTimer += Time.deltaTime;

    HandleRun();
    ApplyMovement();

    //DebugInfo();
  }

}