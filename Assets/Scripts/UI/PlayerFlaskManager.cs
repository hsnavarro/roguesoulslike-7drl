using UnityEngine;
using UnityEngine.UI;

public class PlayerFlaskManager : MonoBehaviour {
    private Text flaskText;

    private PlayerStats playerStats;

    private void Start() {
        flaskText = GameObject.FindGameObjectWithTag("FlaskImageText").GetComponent<Text>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update() {
        flaskText.text = playerStats.flasksCarried.ToString() + " / " + playerStats.flasksCapacity.ToString();
    }
}
