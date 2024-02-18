public class HealthSkill : Skill
{
    public HealthSkill(int level = 0) : base(level)
    {
    }

    public override void ChangePlayerParameter(PlayerParameters playerParameters)
    {
        playerParameters.Health.Level = Level;
    }
}
