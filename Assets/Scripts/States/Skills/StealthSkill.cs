using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthSkill : Skill
{
    public StealthSkill(int level = 0) : base(level)
    {
    }

    public override void ChangePlayerParameter(PlayerParameters playerParameters)
    {
        playerParameters.AdditionalNoise.Level = Level;
    }
}
