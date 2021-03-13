using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatBarManager : MonoBehaviour {
  [SerializeField]
  private EnemyStats enemyStats;

  private Transform resilienceBarPivot;

  private RectTransform healthBarRectangle;
  private RectTransform shieldBarRectangle;

  private Slider healthBarSlider;
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

    resilienceBarPivot = GameObject.FindGameObjectWithTag("ResiliencePivotEnemy").GetComponent<Transform>();

    healthBarRectangle = GameObject.FindGameObjectWithTag("HealthBarEnemy").GetComponent<RectTransform>();
    healthBarSlider = GameObject.FindGameObjectWithTag("HealthBarEnemy").GetComponent<Slider>();

    shieldBarRectangle = GameObject.FindGameObjectWithTag("ShieldBarEnemy").GetComponent<RectTransform>();
    shieldBarSlider = GameObject.FindGameObjectWithTag("ShieldBarEnemy").GetComponent<Slider>();
  }

  private void Update() {
    resilienceBarPivot.rotation = resilienceBarRotation;

    // resilienceBarPivot.LookAt(Camera.main.transform.position);

    healthBarRectangle.sizeDelta = new Vector2(enemyStats.enemyResilience.maxHealth, healthBarRectangle.sizeDelta.y);
    shieldBarRectangle.sizeDelta = new Vector2(enemyStats.enemyResilience.maxShield, shieldBarRectangle.sizeDelta.y);

    healthBarSlider.value = GetHealthBarFillRatio();
    shieldBarSlider.value = GetShieldBarFillRatio();
  }
}
