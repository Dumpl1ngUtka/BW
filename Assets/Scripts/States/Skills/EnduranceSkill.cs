public class EnduranceSkill : Skill
{
    public EnduranceSkill(int level = 0) : base(level)
    {
    }

    public override void ChangePlayerParameter(PlayerParameters playerParameters)
    {
        playerParameters.Stamina.Level += Level;
    }
}
