using System.Numerics;
using Core;

public class RangedAttack : SkillEntity
{
    private const float COOLDOWN = .5f;
    private bool _isReady = true;

    public override void Invoke(Vector2 initialPosition, Vector2 targetPosition)
    {
        if (_isReady)
        {
            _isReady = false;
            var aimDirection = targetPosition - initialPosition;
            var bullet = EntityManager.Create<Bullet>(initialPosition);
            bullet.GetComponent<BulletBehaviour>()?.Direction = aimDirection;

            Timers.CreateTimer(1f, () => bullet.Destroy());
            Timers.CreateTimer(COOLDOWN, () => _isReady = true);
        }
    }
}

