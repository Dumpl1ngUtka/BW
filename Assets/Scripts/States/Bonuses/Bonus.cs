public abstract class Bonus
{
    public int Level { get; private set; }
    public bool IsEquip { get; private set; } = false;

    public Bonus(int level)
    {
        Level = level;
    }

    public void SetEquip(bool isEquip)
    {
        IsEquip = isEquip;
    }

    public abstract void ChangeParameter(PlayerParameters playerParameters);
}
