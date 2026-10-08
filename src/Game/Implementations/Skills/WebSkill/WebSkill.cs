using System.Numerics;
using Core;

public class WebSkill : SkillEntity
{
    public WebSkill()
    {
        Name = "Web";
        Cooldown = 5f;
        Range = 200f;
    }

    protected override void InvokeSkill(Vector2 initialPosition, Vector2 targetPosition)
    {
        var web = EntityManager.Create<SpiderWeb>(targetPosition);
        Timers.CreateTimer(3f, () => web.Destroy());
    }
}
