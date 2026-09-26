using System.Numerics;
using Core;

public class RangedAttack : SkillEntity
{
    public RangedAttack()
    {
        _cooldown = .5f;
        Name = "Ranged Attack";
    }

    protected override void InvokeSkill(Vector2 initialPosition, Vector2 targetPosition)
    {
        var aimDirection = targetPosition - initialPosition;
        var bullet = EntityManager.Create<Bullet>(initialPosition);
        bullet.GetComponent<BulletBehaviour>()?.Direction = aimDirection;

        Timers.CreateTimer(1f, () => bullet.Destroy());
    }
}

