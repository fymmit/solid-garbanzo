using System.Numerics;
using Core;

public class Bullet : Entity
{
    public Bullet()
    {
        Attach<BulletBehaviour>();
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

    private float _speed = 50f;

    public override void Update(float delta)
    {
        Parent.Position += Direction * _speed * delta;
        var enemies = EntityManager.Find<Enemy>();
        foreach (var go in enemies)
        {
            var enemy = (Enemy)go;
            var enemyCollider = enemy.GetComponent<ICollidable>();
            if (enemyCollider is null) continue;
            if (Geometry.CheckCollisionCircles(Parent.Position, 12f, enemyCollider.ColliderPosition, enemyCollider.ColliderRadius))
            {
                Console.WriteLine("Bullet hit enemy");
                enemy.GetComponent<IDamageable>()?.TakeDamage(10);
                Parent.Destroy();
            }
        }
    }
}
