using System.Numerics;
using Core;
using Events;

public class Enemy : Entity
{
    public Enemy()
    {
        Radius = 24f;
        Attach<EnemyBehaviour>();
    }
}

public class EnemyBehaviour : Behaviour, IDamageable, IHarmful
{
    private float _speed = 100f;

    public int Damage => 10;

    public int Health { get; private set; } = 20;

    private Player? _target;

    public override void Ready()
    {
        _target = _entityManager.Find<Player>().FirstOrDefault();
    }

    public override void Update(float delta)
    {
        var direction = Vector2.Zero;
        if (_target is not null)
        {
            direction = _target.Position - Parent.Position;
            direction /= direction.Length();
        }
        Parent.Position += direction * delta * _speed;
        Parent.Rotation = MathF.Atan2(direction.Y, direction.X) * (180f / MathF.PI);
    }

    public void TakeDamage(int amount)
    {
        DamageEventChannel.InvokeDamageEvent(Parent, amount);
        Health -= amount;
        if (Health <= 0)
        {
            DeathEventChannel.InvokeDeathEvent(Parent);
            Parent.Destroy();
        }
    }
}
