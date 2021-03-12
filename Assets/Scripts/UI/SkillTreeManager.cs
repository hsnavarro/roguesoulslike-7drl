using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{

  [SerializeField]
  private Slider healthBarSlider;
  [SerializeField]
  private Slider flaskCapacityBarSlider;
  [SerializeField]
  private Slider staminaBarSlider;

  [SerializeField]
  private PlayerSkillTree playerSkillTree;

  private float GetHealthBarFillRatio()
  {
    return playerSkillTree.healthBarProgression / playerSkillTree.healthBarSize;
  }

  private float GetFlaskCapacityBarFillRatio()
  {
    return playerSkillTree.flasksCapacityBarProgression / playerSkillTree.flasksCapacityBarSize;
  }

  private float GetStaminaBarFillRatio()
  {
    return playerSkillTree.staminaBarProgression / playerSkillTree.staminaBarSize;
  }

  private void Update()
  {
    healthBarSlider.value = GetHealthBarFillRatio();
    flaskCapacityBarSlider.value = GetFlaskCapacityBarFillRatio();
    staminaBarSlider.value = GetStaminaBarFillRatio();
  }
}