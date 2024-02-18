using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingSkill : Skill
{
    public HearingSkill(int level = 0) : base(level)
    {
    }

    public override void ChangePlayerParameter(PlayerParameters playerParameters)
    {
        playerParameters.SoundRange.Level = Level;
    }
}
