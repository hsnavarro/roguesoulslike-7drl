using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  [HideInInspector]
  public Vector3 playerDirection = Vector3.zero;
  [HideInInspector]
  public bool isRunButtonActive = false;

  [HideInInspector]
  public bool isDashing = false;
  [HideInInspector]
  public Vector3 dashDirection = Vector3.zero;
  [HideInInspector]
  public float dashTimer = 0f;

  [Header("Player References")]
  [SerializeField]
  private Animator playerAnimator;
  [SerializeField]
  private CharacterController playerCharacterController;
  [SerializeField]
  private PlayerStats playerStats;
  [SerializeField]
  private PlayerController playerController;

  private Vector3 playerVelocity = Vector3.zero; 

  private float staminaDelayTimer = 0f;

  public void handleDash() {
    if (isDashing) {
      dashTimer += Time.deltaTime;
      if (dashTimer > playerStats.dashDuration) {
        isDashing = false;
        playerAnimator.SetBool("Dashing", false);
      }
    }
  }
  
  public void RestartStaminaRechargeTimer() {
    staminaDelayTimer = 0f;
  }

  public void DecreaseStamina(float value) {
    playerStats.currentStamina = Mathf.Max(0f,
      playerStats.currentStamina - value);
  }

  private void updateLayer() {
    if (isDashing) gameObject.layer = (int)Layers.PLAYER_DASHING;
    else gameObject.layer = (int)Layers.PLAYER;
  }

  private void ApplyMovement() {
    if (playerController.IsActing) return;

    handleDash();
    updateLayer();

    playerStats.isDashing = isDashing;

    if (isDashing) playerVelocity = dashDirection * playerStats.dashSpeed;
    else playerVelocity = playerDirection * playerStats.currentSpeed;

    playerCharacterController.Move(playerVelocity * Time.deltaTime);
  }

  private void IncreaseStamina() {
    if (staminaDelayTimer >= playerStats.staminaRechargeDelay) {      
      playerStats.currentStamina = Mathf.Min(playerStats.maxStamina, 
        playerStats.currentStamina + Time.deltaTime * playerStats.staminaRechargeRate);
    }
  }

  private void HandleRun() {
    bool isRunning = isRunButtonActive && playerStats.currentStamina > 0f;
    bool isMoving = playerDirection != Vector3.zero;

    // Animation update
    playerAnimator.SetInteger("MoveState", isMoving ? (isRunning ? 2 : 1) : 0);

    if (playerController.IsActing) return;

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
    if (isDashing) targetPosition = dashDirection;

    targetPosition += transform.position;
    transform.LookAt(targetPosition);
  }

  private void Update() {
    if (playerStats.currentStamina != playerStats.maxStamina) staminaDelayTimer += Time.deltaTime;

    HandleRun();
    ApplyMovement();

    // Debug.Log("Player velocity vector " + playerVelocity);
  }

}