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

    private Vector3 instantNormalizedPlayerVelocity = Vector3.zero;
    private Vector3 instantPlayerVelocity = Vector3.zero;
    private bool isRunButtonActive = false;
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
        bool isRunningModeActive = isRunButtonActive && playerStats.Stamina > 0f;
        bool isMoving = instantNormalizedPlayerVelocity != Vector3.zero;

        if (isRunningModeActive)
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

        if ((!isRunningModeActive || !isMoving) && staminaDelayTimer >= playerStats.staminaRechargeDelay)
        {
            playerStats.Stamina = Mathf.Min(playerStats.maxStamina,
                 playerStats.Stamina + Time.deltaTime * playerStats.staminaRechargeRate);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>().normalized;
            instantNormalizedPlayerVelocity = new Vector3(input.x, 0, input.y);
        }

        if (context.canceled) instantNormalizedPlayerVelocity = Vector3.zero;
    }

    private void HandleMove()
    {
        instantPlayerVelocity = instantNormalizedPlayerVelocity * playerStats.speed;
        playerController.Move(instantPlayerVelocity * Time.deltaTime);
    }

    private void DebugInfo()
    {
        Debug.print("Player velocity vector " + instantPlayerVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        HandleRun();
        HandleMove();

        //DebugInfo();
    }
}
