public abstract class Skill
{
    private readonly int _maxLevel = 10;
    private int _level;
    public int Level
    {
        get { return _level; }
        set
        {
            int newLevel = value + _level;
            if (newLevel >= 0 || newLevel <= _maxLevel)
                _level = newLevel;
        }
    }

    public bool IsMaxLevel = false;
    public Skill(int level = 0)
    {
        Level = level;
    }

    public void LevelUp(int value = 1)
    {
        Level += value;
        if (Level == _maxLevel)
            IsMaxLevel = true;
    }

    public abstract void ChangePlayerParameter(PlayerParameters playerParameters);
}