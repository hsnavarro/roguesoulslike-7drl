using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{

  public Slider healthBarSlider;
  public Slider shieldBarSlider;
  public Slider staminaBarSlider;

  public PlayerSkillTree playerSkillTree;

  private float GetHealthBarFillRatio()
  {
    return playerSkillTree.healthBarProgression / playerSkillTree.healthBarSize;
  }

  private float GetShieldBarFillRatio()
  {
    return playerSkillTree.shieldBarProgression / playerSkillTree.shieldBarSize;
  }

  private float GetStaminaBarFillRatio()
  {
    return playerSkillTree.staminaBarProgression / playerSkillTree.staminaBarSize;
  }

  private void Update()
  {
    healthBarSlider.value = GetHealthBarFillRatio();
    shieldBarSlider.value = GetShieldBarFillRatio();
    staminaBarSlider.value = GetStaminaBarFillRatio();
  }
}