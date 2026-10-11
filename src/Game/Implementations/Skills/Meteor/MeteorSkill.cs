using System.Numerics;
using Core;

public class MeteorSkill : SkillEntity
{
    public MeteorSkill()
    {
        Name = "Meteor";
        Cooldown = 4f;
        EffectDelay = .5f;
        Range = 300f;
    }

    protected override void InvokeSkill(Vector2 initialPosition, Vector2 targetPosition)
    {
        var meteor = EntityManager.Create<Meteor>(targetPosition);

        Timers.CreateTimer(0.5f, () => meteor.Destroy());
    }
}

