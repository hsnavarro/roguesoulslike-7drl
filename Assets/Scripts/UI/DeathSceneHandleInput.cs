using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathSceneHandleInput : MonoBehaviour {

  public void OnInput(InputAction.CallbackContext context) {
    if (context.started) {
        SceneManager.LoadScene("Navarro");
    }
  }
  
}
