using System.Numerics;
using Core;

public class Bullet : Entity
{
    public Bullet()
    {
        Radius = 12f;
        Attach<BulletBehaviour>();
        Attach(new Duration(1f));
    }
}

public class BulletBehaviour : Behaviour
{
    public Vector2 Direction
    {
        get; set
        {
            field = Vector2.Normalize(value);
        }
    }

    private float _speed = 300f;

    public override void Update(float delta)
    {
        Parent.Position += Direction * _speed * delta;
        var enemies = EntityManager.Find<Enemy>();
        foreach (var enemy in enemies)
        {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                if (Geometry.CheckCollisionCircles(Parent.Position, Parent.Radius, enemy.Position, enemy.Radius))
                {
                    damageable.TakeDamage(10);
                    Parent.Destroy();
                }
            }
        }
    }
}
