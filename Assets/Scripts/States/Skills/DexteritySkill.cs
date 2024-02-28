using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexteritySkill : Skill
{
    public DexteritySkill(int level = 0) : base(level)
    {
    }

    public override void ChangePlayerParameter(PlayerParameters playerParameters)
    {
        playerParameters.Stamina.Level += Level;
    }
}
