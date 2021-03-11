using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBarManager : MonoBehaviour {
  public EnemyStats enemyStats;
  public Transform defenseBarPivot;

  public RectTransform healthBarRectangle;
  public RectTransform shieldBarRectangle;

  public Slider healthBarSlider;
  public Slider shieldBarSlider;

  private float GetHealthBarFillRatio() {
    return enemyStats.defense.currentHealth / enemyStats.defense.maxHealth;
  }

  private float GetShieldBarFillRatio() {
    return enemyStats.defense.currentShield / enemyStats.defense.maxShield;
  }
  
  private void Update() {
    //defenseBarPivot.LookAt(Camera.main.transform.position);

    healthBarRectangle.sizeDelta = new Vector2(enemyStats.defense.maxHealth, healthBarRectangle.sizeDelta.y);
    shieldBarRectangle.sizeDelta = new Vector2(enemyStats.defense.maxShield, shieldBarRectangle.sizeDelta.y);

    healthBarSlider.value = GetHealthBarFillRatio();
    shieldBarSlider.value = GetShieldBarFillRatio();
  }
}
