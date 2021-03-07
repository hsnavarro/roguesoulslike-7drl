using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
   
    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_HorizontalAxis), 0f, Input.GetAxisRaw(GameConstants.k_VerticalAxis));
        move = move.normalized;

        return move;
    }

    public bool GetRunButton()
    {
        return Input.GetButton(GameConstants.k_RunButton);
    }
}
