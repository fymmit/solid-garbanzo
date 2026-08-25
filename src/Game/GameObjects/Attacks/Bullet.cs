using Raylib_cs;
using System.Numerics;
using Game.Traits;

namespace Game.GameObjects.Attacks;

public class Bullet : GameObject
{
    public Vector2 Direction
    {
        get; set
        {
            field = value / value.Length();
        }
    }
    private float _speed = 50f;

    public void SetDirection(Vector2 direction)
    {
        Direction = direction / direction.Length();
    }

    public Bullet()
    {
        Components = [
            new Renderer(this, Raylib_cs.Color.Magenta, 12f, Shape.Circle)
        ];
    }

    public override void Ready() { }

    public override void Update(float delta)
    {
        Position += Direction * _speed * delta;
        var enemies = GameObjectManager.GameObjects.Where(go => go is Enemy);
        foreach (var go in enemies)
        {
            var enemy = (Enemy)go;
            if (Raylib.CheckCollisionCircles(Position, 12f, enemy.ColliderPosition, enemy.ColliderRadius))
            {
                Console.WriteLine("Bullet hit enemy");
                enemy.GetComponent<IDamageable>()?.TakeDamage(enemy.Damage);
                Destroy();
            }
        }
    }
}
