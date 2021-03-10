using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleItem : MonoBehaviour {

    public PlayerStats playerStats;

    [SerializeField]
    private float healthRegeneration;
    [SerializeField]
    private float shieldRegeneration;
    
    public void Use() {
      playerStats.defense.currentHealth += healthRegeneration;
      playerStats.defense.currentShield += shieldRegeneration;     
    }
}
