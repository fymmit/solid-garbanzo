using Raylib_cs;
using System.Numerics;
using Game.Traits;

namespace Game.GameObjects.Attacks;

public class Bullet : GameObject
{
    public Bullet()
    {
        Attach<BulletBehaviour>();
        Renderer = new Renderer(this, Raylib_cs.Color.Magenta, 12f, Shape.Circle);
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
        if (Parent is null) return;

        Parent.Position += Direction * _speed * delta;
        var enemies = GameObjectManager.GameObjects.Where(go => go is Enemy);
        foreach (var go in enemies)
        {
            var enemy = (Enemy)go;
            var enemyCollider = enemy.GetComponent<ICollidable>();
            if (enemyCollider is null) continue;
            if (Raylib.CheckCollisionCircles(Parent.Position, 12f, enemyCollider.ColliderPosition, enemyCollider.ColliderRadius))
            {
                Console.WriteLine("Bullet hit enemy");
                enemy.GetComponent<IDamageable>()?.TakeDamage(10);
                Parent.Destroy();
            }
        }
    }
}
