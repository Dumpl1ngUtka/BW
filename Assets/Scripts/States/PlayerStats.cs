using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerSkillLevels _playerSkillLevels;
    public PlayerParameters PlayerParameters;

    private void Awake()
    {
        _playerSkillLevels.SkillsChanged += ChangePlayerParameters;
    }

    private void ChangePlayerParameters()
    {
        var newPlayerParameters = new PlayerParameters();
        foreach (var skill in _playerSkillLevels.Skills)
            skill.ChangePlayerParameter(newPlayerParameters);
        PlayerParameters = newPlayerParameters;    
    }
}
