using System.Numerics;
using Core;
using Events;

public class AutoTargeting : Behaviour
{
    private Enemy? _target;
    private const float SHOOT_INTERVAL = 0.5f;
    private float _shootTimer = SHOOT_INTERVAL;

    public override void Ready()
    {
        DeathEventChannel.DeathEvent += OnDeathEvent;
    }

    public override void Update(float delta)
    {
        if (_target is null)
        {
            var closestEnemy = EntityManager
                .Find<Enemy>()
                .OrderBy(e => Vector2.Distance(Parent.Position, e.Position))
                .FirstOrDefault();

            _target = closestEnemy;
        }

        _shootTimer -= delta;
        if (_shootTimer <= 0)
        {
            _shootTimer = SHOOT_INTERVAL;
            Shoot();
        }

    }

    private void Shoot()
    {
        if (_target is null)
        {
            return;
        }
        var aimDirection = _target.Position - Parent.Position;
        var bullet = EntityManager.Create<Bullet>(Parent.Position);
        bullet.GetComponent<BulletBehaviour>()?.Direction = aimDirection;
    }

    void OnDeathEvent(Entity? target)
    {
        if (target == _target)
        {
            _target = null;
        }
    }

    public override void OnDestroy()
    {
        DeathEventChannel.DeathEvent -= OnDeathEvent;
    }
}
