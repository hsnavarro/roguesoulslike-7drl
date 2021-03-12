using UnityEngine;
using UnityEngine.UI;

public class FlaskManager : MonoBehaviour
{
    public Text flaskText;
    private PlayerStats playerStats;

    void Start() 
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    void Update()
    {
        flaskText.text = playerStats.flasksCarried.ToString() + " / " + playerStats.flasksCapacity.ToString();
    }
}
