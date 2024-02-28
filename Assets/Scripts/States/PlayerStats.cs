using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerSkillLevels _playerSkillLevels;
    public PlayerParameters PlayerParameters;

    public event Action ParametersChanged;

    private void Awake()
    {
        PlayerParameters = new PlayerParameters();
        _playerSkillLevels.SkillsChanged += ChangePlayerParameters;
    }

    private void OnDisable()
    {
        _playerSkillLevels.SkillsChanged -= ChangePlayerParameters;
    }

    private void ChangePlayerParameters()
    {
        var newPlayerParameters = new PlayerParameters();
        foreach (var skill in _playerSkillLevels.Skills)
            skill.ChangePlayerParameter(newPlayerParameters);
        PlayerParameters = newPlayerParameters;
        ParametersChanged?.Invoke();
    }
}
