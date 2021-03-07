using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    [SerializeField]
    private float playerNormalSpeed;

    private PlayerInputHandler inputHandler;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 instantNormalizedVelocity = inputHandler.GetMoveInput();

        Vector3 instantVelocity = instantNormalizedVelocity * playerNormalSpeed;  

        print("Player velocity vector " + instantVelocity);

        characterController.Move(instantVelocity * Time.deltaTime);
    }
}
