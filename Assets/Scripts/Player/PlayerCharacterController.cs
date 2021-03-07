using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    private PlayerInputHandler inputHandler;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private PlayerStats playerStats;

    private float staminaDelayTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 instantNormalizedVelocity = inputHandler.GetMoveInput();

        bool isRunButtonActive = inputHandler.GetRunButton();

        bool isRunning = isRunButtonActive && playerStats.Stamina > float.Epsilon;

        Vector3 instantVelocity;

        if(isRunning)
        {
            playerStats.speed = playerStats.runSpeed;
            playerStats.Stamina = Mathf.Max(0f,
                playerStats.Stamina - Time.deltaTime * playerStats.staminaRunDecreaseRate);

            if (playerStats.Stamina <= float.Epsilon) staminaDelayTimer = 0f;
        } else
        {
            playerStats.speed = playerStats.normalSpeed;
            if(staminaDelayTimer >= playerStats.staminaRechargeDelay) playerStats.Stamina =
                Mathf.Min(playerStats.maxStamina,
                playerStats.Stamina + Time.deltaTime * playerStats.staminaRechargeRate);
        }

        staminaDelayTimer += Time.deltaTime;

        instantVelocity = instantNormalizedVelocity * playerStats.speed;

        Debug.print("Player velocity vector " + instantVelocity);
        Debug.print("Player HP " + playerStats.HP);
        Debug.print("Player Stamina " + playerStats.Stamina);

        characterController.Move(instantVelocity * Time.deltaTime);
    }
}
