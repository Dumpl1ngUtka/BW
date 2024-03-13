using System.Collections.Generic;

public class PlayerBonusHolder
{
    public int MaxBonusCount = 5;
    public List<Bonus> Bonuses { get; private set; } = new();

    public int GetEmptyBonusCellsCount() 
	{
        var activeBonusCount = 0;
        foreach (var bonus in Bonuses)
            if (bonus.IsEquip)
                activeBonusCount++;
        return MaxBonusCount - activeBonusCount;
    }

    public void AddNewBonus(Bonus bonus)
    {
        Bonuses.Add(bonus);
    }

    public void Equip(Bonus bonus)
    {
        if (GetEmptyBonusCellsCount() > 0)
            bonus.SetEquip(true);
    }

    public void RemoveBonus(Bonus bonus)
    {
        bonus.SetEquip(false);
    }

    public int TryGetBonusLevel(Bonus bonus)
    {
        foreach (var item in Bonuses)
            if (item == bonus)      //mb doesn't work
                return item.Level;
        return 0;
    }
}
