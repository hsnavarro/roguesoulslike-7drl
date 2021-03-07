using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour
{
    public HUDManager HUDManager;
    public PlayerStats playerStats;
    public Slider HPBar;
    public Text HPText;

    private float GetBarFillRatio()
    {
        return playerStats.HP / playerStats.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = GetBarFillRatio();
        if(HUDManager.showText) HPText.text = Mathf.FloorToInt(playerStats.HP).ToString();
    }
}
