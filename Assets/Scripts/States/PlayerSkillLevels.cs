using System;
using UnityEngine;

public class PlayerSkillLevels : MonoBehaviour
{
    public readonly Skill[] Skills = new Skill[6];

    public HealthSkill Health = new(0);
    public EnduranceSkill Endurance = new(0);
    public StrengthSkill Strength = new(0);
    public DexteritySkill Dexterity = new(0);
    public HearingSkill Hearing = new(0);
    public StealthSkill Stealth = new(0);

    public event Action SkillsChanged;

    public int LevelPoints { get; private set; } = 12;

    private void Awake()
    {
        InitSkills();
    }

    public int ResetSkills()
    {
        int skillPointsCount = 0;
        foreach (var skill in Skills)
        {
            skillPointsCount += skill.Level;
            skill.Level = 0;
        }
        SkillsChanged?.Invoke();
        return skillPointsCount;
    }

    public bool LevelUp(Skill skill, int value = 1)
    {
        if (skill.IsMaxLevel)
            return false;
        if (value > LevelPoints)
            value = LevelPoints;
        LevelPoints -= value;
        skill.LevelUp(value);
        SkillsChanged?.Invoke();
        return true;
    }

    private void InitSkills()
    {
        Skills[0] = Health;
        Skills[1] = Endurance;
        Skills[2] = Strength;
        Skills[3] = Dexterity;
        Skills[4] = Hearing;
        Skills[5] = Stealth;
    }
}