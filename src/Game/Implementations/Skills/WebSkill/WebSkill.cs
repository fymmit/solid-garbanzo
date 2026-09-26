using System.Numerics;
using Core;

public class WebSkill : SkillEntity
{
    public WebSkill()
    {
        _cooldown = 5f;
        Name = "Web";
    }

    protected override void InvokeSkill(Vector2 initialPosition, Vector2 targetPosition)
    {
        var web = EntityManager.Create<SpiderWeb>(targetPosition);
        Timers.CreateTimer(3f, () => web.Destroy());
    }
}
