using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(InputAction.CallbackContext callbackContext) {
        SceneManager.LoadScene("Scenes/Naum");
    }
}