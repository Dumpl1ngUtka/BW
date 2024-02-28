using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthSkill : Skill
{
    public StrengthSkill(int level = 0) : base(level)
    {
    }

    public override void ChangePlayerParameter(PlayerParameters playerParameters)
    {
        playerParameters.Damage.Level += Level;
    }
}
