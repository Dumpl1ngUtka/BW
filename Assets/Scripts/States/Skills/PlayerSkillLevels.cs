using System;

public class PlayerSkillLevels
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

    public PlayerSkillLevels()
    {
        InitSkills();
    }

    public void ResetSkills()
    {
        foreach (var skill in Skills)
            LevelPoints += skill.Reset();
        SkillsChanged?.Invoke();
    }

    public bool TryLevelUp(Skill skill, int value = 1)
    {
        if (!skill.TryLevelUp(value))
            return false;

        LevelPoints -= value;
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