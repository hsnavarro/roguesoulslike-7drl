using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterController playerController;
    [SerializeField]
    private PlayerStats playerStats;

    private float staminaDelayTimer = 0f;

    private Vector3 normalizedPlayerVelocity = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    private bool isRunButtonActive = false;

    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerStats.damage = playerStats.heavyAttackDamage;
            playerStats.Stamina = Mathf.Max(0f, playerStats.Stamina - playerStats.heavyAttackStaminaDecrease);
        }

    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerStats.damage = playerStats.lightAttackDamage;
            playerStats.Stamina = Mathf.Max(0f, playerStats.Stamina - playerStats.lightAttackStaminaDecrease);
        }
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

        if ((!isRunning || !isMoving) && staminaDelayTimer >= playerStats.staminaRechargeDelay) 
        {
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
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>().normalized;
            normalizedPlayerVelocity = new Vector3(input.x, 0, input.y);
        }

        if (context.canceled) normalizedPlayerVelocity = Vector3.zero;
    }

    private void ApplyMovement()
    {
        playerVelocity = normalizedPlayerVelocity * playerStats.speed;
        playerController.Move(playerVelocity * Time.deltaTime);
    }

    private void DebugInfo()
    {
        Debug.print("Player velocity vector " + instantPlayerVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        HandleRun();
        ApplyMovement();

        //DebugInfo();
    }
}
