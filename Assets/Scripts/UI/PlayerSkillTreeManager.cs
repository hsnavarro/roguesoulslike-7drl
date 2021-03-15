using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillTreeManager : MonoBehaviour {
  
  [Header("Slider References")]
  [SerializeField]
  private Slider healthProgressBarSlider;
  [SerializeField]
  private Slider flaskCapacityProgressBarSlider;
  [SerializeField]
  private Slider staminaProgressBarSlider;

  [SerializeField]
  private LevelUpMessage healthLevelUp;
  [SerializeField]
  private LevelUpMessage staminaLevelUp;
  [SerializeField]
  private LevelUpMessage flaskLevelUp;

  private PlayerSkillTree playerSkillTree;

  void Start() {
    playerSkillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillTree>();

    healthProgressBarSlider = GameObject.FindGameObjectWithTag("SkillTreeHealthProgressBar").GetComponent<Slider>();
    staminaProgressBarSlider = GameObject.FindGameObjectWithTag("SkillTreeStaminaProgressBar").GetComponent<Slider>();
    flaskCapacityProgressBarSlider = GameObject.FindGameObjectWithTag("SkillTreeFlaskCapacityProgressBar").GetComponent<Slider>();
  }

  void Update() {
    healthProgressBarSlider.value = GetHealthBarFillRatio();
    flaskCapacityProgressBarSlider.value = GetFlaskCapacityBarFillRatio();
    staminaProgressBarSlider.value = GetStaminaBarFillRatio();
  }

  private float GetHealthBarFillRatio() {
    return playerSkillTree.healthBarProgression / playerSkillTree.healthBarSize;
  }

  private float GetFlaskCapacityBarFillRatio() {
    return playerSkillTree.flasksHealBarProgression / playerSkillTree.flasksHealBarSize;
  }

  private float GetStaminaBarFillRatio() {
    return playerSkillTree.staminaBarProgression / playerSkillTree.staminaBarSize;
  }

  public void HealthLevelUp() {
    healthLevelUp.Show();
  }

  public void StaminaLevelUp() {
    staminaLevelUp.Show();
  }

  public void FlaskLevelUp() {
    flaskLevelUp.Show();
  }
}