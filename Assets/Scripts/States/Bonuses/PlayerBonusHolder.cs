using System.Collections.Generic;
using UnityEngine;

public class PlayerBonusHolder : MonoBehaviour
{
    public List<Bonus> Bonuses { get; private set; } = new();

    public void AddBonus(Bonus bonus)
    {
        Bonuses.Add(bonus);
    }
}
