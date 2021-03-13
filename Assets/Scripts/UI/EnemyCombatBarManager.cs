using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatBarManager : MonoBehaviour {
  [SerializeField]
  private EnemyStats enemyStats;

  private Transform resilienceBarPivot;

  [SerializeField]
  private RectTransform healthBar;
  private Slider healthBarSlider;

  [SerializeField]
  private RectTransform shieldBar;
  private Slider shieldBarSlider;

  
  Quaternion resilienceBarRotation;

  private float GetHealthBarFillRatio() {
    return enemyStats.enemyResilience.currentHealth / enemyStats.enemyResilience.maxHealth;
  }

  private float GetShieldBarFillRatio() {
    return enemyStats.enemyResilience.currentShield / enemyStats.enemyResilience.maxShield;
  }

  private void Start() {
    resilienceBarRotation = Quaternion.Euler(0f, -38f, 0f);

    healthBarSlider = healthBar.GetComponent<Slider>();
    shieldBarSlider = shieldBar.GetComponent<Slider>();
  }

  private void Update() {
    transform.rotation = resilienceBarRotation;

    // resilienceBarPivot.LookAt(Camera.main.transform.position);

    healthBar.sizeDelta = new Vector2(enemyStats.enemyResilience.maxHealth, healthBar.sizeDelta.y);
    shieldBar.sizeDelta = new Vector2(enemyStats.enemyResilience.maxShield, shieldBar.sizeDelta.y);

    healthBarSlider.value = GetHealthBarFillRatio();
    shieldBarSlider.value = GetShieldBarFillRatio();
  }
}
