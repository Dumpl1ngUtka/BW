public abstract class Skill
{
    private readonly int _maxLevel = 10;
    public int Level { get; private set; }
 
    public Skill(int level = 0)
    {
        Level = level;
    }

    public bool TryLevelUp(int value = 1)
    {
        if (value <= 0)
            return false;

        int newLevel = value + Level;
        if (newLevel > _maxLevel)
            return false;

        Level = newLevel; 
        return true;
    }

    public int Reset()
    {
        var points = Level;
        Level = 0;
        return points;
    }

    public abstract void ChangePlayerParameter(PlayerParameters playerParameters);
}