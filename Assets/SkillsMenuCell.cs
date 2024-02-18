using UnityEngine;
using UnityEngine.UI;

public class SkillsMenuCell : MonoBehaviour
{
    [SerializeField] private Image _selectBox;
    [SerializeField] private Image _progressBox;
    public Skill Skill { get; private set; }
    public int AdditionalLevel { get; private set; }

    public void Init(Skill skill)
    {
        Skill = skill;
    }
    public void SetSelect(bool isSelect)
    {
        _selectBox.enabled = isSelect;
    }

    public void RemoveAdditionalLevels()
    {
        AdditionalLevel = 0;
    }

    public bool TryAddLevel(int value)
    {
        if (value + Skill.Level + AdditionalLevel > 10)
            return false;

        AdditionalLevel += value;
        return true;
    }

    public bool TryRemoveLevel(int value)
    {
        if (AdditionalLevel - value < 0)
            return false;

        AdditionalLevel -= value;
        return true;
    }

    public void Render()
    {
        _progressBox.fillAmount = (float)(Skill.Level + AdditionalLevel) / 10;
    }
}
