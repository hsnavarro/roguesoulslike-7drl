using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private PlayerStats playerStats;

    private float staminaDelayTimer = 0f;

    private Vector3 normalizedPlayerVelocity = Vector3.zero;
    private Vector3 instantPlayerVelocity = Vector3.zero;
    private bool isRunButtonActive = false;

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.canceled) isRunButtonActive = false;
        else if (context.performed) isRunButtonActive = true;
    }

    private void HandleRun()
    {
        bool isRunning = isRunButtonActive && playerStats.Stamina > 0f;
        bool isMoving = normalizedPlayerVelocity != Vector3.zero;

        if (isRunning)
        {
            playerStats.speed = playerStats.runSpeed;

            if (isMoving)
            {
                playerStats.Stamina = Mathf.Max(0f,
                    playerStats.Stamina - Time.deltaTime * playerStats.staminaRunDecreaseRate);

                staminaDelayTimer = 0f;
            }
        }
        else
        {
            playerStats.speed = playerStats.normalSpeed;
        }

        staminaDelayTimer += Time.deltaTime;

        if ((!isRunning || !isMoving) && staminaDelayTimer >= playerStats.staminaRechargeDelay) {
            playerStats.Stamina = Mathf.Min(playerStats.maxStamina,
                 playerStats.Stamina + Time.deltaTime * playerStats.staminaRechargeRate);
        }

        // Rotation update
        Vector3 targetPosition = new Vector3(normalizedPlayerVelocity.x, 0f, normalizedPlayerVelocity.z);
        targetPosition += transform.position;
        transform.LookAt(targetPosition);

        // Animation update
        anim.SetInteger("MoveState", isMoving ? (isRunning ? 2 : 1) : 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>().normalized;
            normalizedPlayerVelocity = new Vector3(input.x, 0, input.y);
        }

        if (context.canceled) normalizedPlayerVelocity = Vector3.zero;
    }

    private void ApplyMovement()
    {
        instantPlayerVelocity = normalizedPlayerVelocity * playerStats.speed;
        characterController.Move(instantPlayerVelocity * Time.deltaTime);
    }

    private void DebugInfo()
    {
        Debug.print("Player velocity vector " + instantPlayerVelocity);
        Debug.print("Player HP " + playerStats.HP);
        Debug.print("Player Stamina " + playerStats.Stamina);
    }

    // Update is called once per frame
    void Update()
    {
        HandleRun();
        ApplyMovement();

        //DebugInfo();
    }
}
