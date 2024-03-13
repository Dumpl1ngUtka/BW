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

    private const string _saveKey = "skills";

    public PlayerSkillLevels()
    {
        Load();
        InitSkills();
    }

    public void ResetSkills()
    {
        foreach (var skill in Skills)
            LevelPoints += skill.Reset();
        SkillsChanged?.Invoke();
        Save();
    }

    public bool TryLevelUp(Skill skill, int value = 1)
    {
        if (!skill.TryLevelUp(value))
            return false;

        LevelPoints -= value;
        SkillsChanged?.Invoke();
        Save();
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

    private void Load()
    {
        var data = SaveSystem.SaveLoadManager.Load<SaveSystem.SkillLevels>(_saveKey);

        Health = new(data.HealthLevel);
        Endurance = new(data.EnduranceLevel);
        Strength = new(data.StrengthLevel);
        Dexterity = new(data.DexterityLevel);
        Hearing = new(data.HearingLevel);
        Stealth = new(data.StealthLevel);
        LevelPoints = data.LevelPoints;
    }

    private void Save()
    {
        SaveSystem.SaveLoadManager.Save(_saveKey, GetCurrentData());
    }

    private SaveSystem.SkillLevels GetCurrentData()
    {
        var data = new SaveSystem.SkillLevels()
        {
            HealthLevel = Health.Level,
            EnduranceLevel = Endurance.Level,
            StrengthLevel = Strength.Level,
            DexterityLevel = Dexterity.Level,
            HearingLevel = Hearing.Level,
            StealthLevel = Stealth.Level,
            LevelPoints = LevelPoints,
        };
        return data;
    }
}