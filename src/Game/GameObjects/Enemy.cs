using System.Numerics;
using Raylib_cs;
using Game.Traits;

namespace Game.GameObjects;

public class Enemy : GameObject
{
    public Enemy()
    {
        Components = [
            Attach<EnemyBehaviour>()
        ];

        Renderer = new Renderer(this, Color.Red, 24f, Shape.Triangle);
    }
}

public class EnemyBehaviour : Behaviour, IDamageable, IHarmful
{
    private float _speed = 10f;

    private Color _color = Color.Red;
    private float _radius = 16f;

    public Vector2 ColliderPosition => Parent!.Position;
    public float ColliderRadius => _radius;
    public int Damage => 10;

    public int Health => 10;

    private Player.Player? _target;

    public override void Update(float delta)
    {
        if (Parent is null) return;
        if (_target is null)
        {
            _target = GameObjectManager.GameObjects.OfType<Player.Player>().FirstOrDefault();
        }

        var direction = Vector2.Zero;
        if (_target is not null)
        {
            direction = _target.Position - Parent.Position;
            direction /= direction.Length();
        }
        Parent.Position += direction * delta * _speed;
    }

    public void TakeDamage(int amount)
    {
        Console.WriteLine("Enemy TakeDamage()");
    }
}

