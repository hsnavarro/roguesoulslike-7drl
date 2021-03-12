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

  private PlayerSkillTree playerSkillTree;

  void Start() {
    playerSkillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkillTree>();
  }

  private float GetHealthBarFillRatio() {
    return playerSkillTree.healthBarProgression / playerSkillTree.healthBarSize;
  }

  private float GetFlaskCapacityBarFillRatio() {
    return playerSkillTree.flasksCapacityBarProgression / playerSkillTree.flasksCapacityBarSize;
  }

  private float GetStaminaBarFillRatio() {
    return playerSkillTree.staminaBarProgression / playerSkillTree.staminaBarSize;
  }

  private void Update() {
    healthProgressBarSlider = GameObject.FindGameObjectWithTag("SkillTreeHealthProgressBar").GetComponent<Slider>();
    staminaProgressBarSlider = GameObject.FindGameObjectWithTag("SkillTreeStaminaProgressBar").GetComponent<Slider>();
    flaskCapacityProgressBarSlider = GameObject.FindGameObjectWithTag("SkillTreeFlaskCapacityProgressBar").GetComponent<Slider>();

    healthProgressBarSlider.value = GetHealthBarFillRatio();
    flaskCapacityProgressBarSlider.value = GetFlaskCapacityBarFillRatio();
    staminaProgressBarSlider.value = GetStaminaBarFillRatio();
  }
}