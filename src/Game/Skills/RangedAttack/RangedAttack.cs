using System.Numerics;
using Core;

public class RangedAttack : SkillEntity
{
    private Cooldown _cooldown = new Cooldown(.5f);

    public RangedAttack()
    {
        Attach(_cooldown);
    }

    public override void Invoke(Vector2 initialPosition, Vector2 targetPosition)
    {
        if (_cooldown.IsReady())
        {
            _cooldown.Reset();
            var aimDirection = targetPosition - initialPosition;
            var bullet = EntityManager.Create<Bullet>(initialPosition);
            bullet.GetComponent<BulletBehaviour>()?.Direction = aimDirection;
        }
    }
}

