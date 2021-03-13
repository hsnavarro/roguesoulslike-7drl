using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  [HideInInspector]
  public Vector3 playerVelocity = Vector3.zero; 

  [HideInInspector]
  public Vector3 playerDirection = Vector3.zero;
  [HideInInspector]
  public bool isRunButtonActive = false;

  [HideInInspector]
  public bool isDashing = false;
  [HideInInspector]
  public Vector3 dashDirection = Vector3.zero;
  [HideInInspector]
  public float lastTimeUsedDash = Mathf.Infinity;
  [HideInInspector]
  public float staminaLastTimeUsed = Mathf.NegativeInfinity;

  [Header("Player References")]
  [SerializeField]
  private Animator playerAnimator;
  [SerializeField]
  private CharacterController playerCharacterController;
  [SerializeField]
  private PlayerStats playerStats;
  [SerializeField]
  private PlayerController playerController;

  public void UpdateDashState() {
    if (Time.time - lastTimeUsedDash > playerStats.dashDuration) {
        playerAnimator.SetBool("Dashing", false);
        lastTimeUsedDash = Mathf.Infinity;
    }
  }

  public void IncreaseStamina() {
    if (Time.time - staminaLastTimeUsed > playerStats.staminaRechargeDelay) {      
      playerStats.currentStamina = Mathf.Min(playerStats.maxStamina, 
        playerStats.currentStamina + Time.deltaTime * playerStats.staminaRechargeRate);
    }
  }

  public void DecreaseStamina(float value) {
    playerStats.currentStamina = Mathf.Max(0f,
      playerStats.currentStamina - value);
  }

  private void ApplyMovement() {
    if (playerController.IsActing) return;

    if (isDashing) {
      playerVelocity = dashDirection * playerStats.dashSpeed;
    } else {
      playerVelocity = playerDirection * playerStats.currentSpeed;
    }

    playerCharacterController.Move(playerVelocity * Time.deltaTime);
  }

  private void ApplyRotation() {
    if (playerController.IsActing) return;

    Vector3 targetPosition = new Vector3(playerDirection.x, 0f, playerDirection.z);
    if (isDashing) targetPosition = dashDirection;

    targetPosition += transform.position;
    transform.LookAt(targetPosition);
  }

  private void UpdateMoveState() {
    bool isRunning = isRunButtonActive && playerStats.currentStamina > 0f;
    bool isMoving = playerDirection != Vector3.zero;

    // Animation update
    playerAnimator.SetInteger("MoveState", isMoving ? (isRunning ? 2 : 1) : 0);
  }

  private void Update() {
    UpdateMoveState();
    UpdateDashState();
    ApplyMovement();
    ApplyRotation();

    // Debug.Log("staminaLastTimeUsed " + staminaLastTimeUsed);
    // Debug.Log("Player velocity vector " + playerVelocity);
  }

}