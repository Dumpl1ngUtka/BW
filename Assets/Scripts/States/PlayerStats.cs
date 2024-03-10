using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerParameters PlayerParameters { get; private set; }
    public PlayerBonusHolder BonusHolder { get; private set; }
    public PlayerSkillLevels PlayerSkillLevels { get; private set; }

    public event Action ParametersChanged;

    private void Awake()
    {
        PlayerSkillLevels = new PlayerSkillLevels();
        PlayerParameters = new PlayerParameters();
        BonusHolder = new PlayerBonusHolder();
    }

    private void OnEnable()
    {
        PlayerSkillLevels.SkillsChanged += ChangePlayerParameters;
    }

    private void OnDisable()
    {
        PlayerSkillLevels.SkillsChanged -= ChangePlayerParameters;
    }

    private void ChangePlayerParameters()
    {
        var newPlayerParameters = new PlayerParameters();
        foreach (var skill in PlayerSkillLevels.Skills)
            skill.ChangePlayerParameter(newPlayerParameters);
        foreach (var bonus in BonusHolder.Bonuses)
            if (bonus.IsEquip)
                bonus.ChangeParameter(newPlayerParameters);
        PlayerParameters = newPlayerParameters;
        ParametersChanged?.Invoke();
    }
}
