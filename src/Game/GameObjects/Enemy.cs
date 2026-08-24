using System.Numerics;
using Raylib_cs;
using Game.Traits;

namespace Game.GameObjects;

public class Enemy : GameObject, IHarmful
{
    private float _speed = 10f;

    private Color _color = Color.Red;
    private float _radius = 16f;

    public Vector2 ColliderPosition => Position;
    public float ColliderRadius => _radius;
    public int Damage => 5;

    private Player.Player? _player;

    public Enemy()
    {
        Components = [
            new Renderer(this, Color.Red, 24f, Shape.Triangle)
        ];
    }

    public override void Ready()
    {
        Console.WriteLine("Enemy ready.");
        _player = Program.GameObjectManager.GameObjects.OfType<Player.Player>().FirstOrDefault();
    }

    public override void Update(float delta)
    {
        var direction = Vector2.UnitX;
        if (_player is not null)
        {
            direction = _player.Position - Position;
            direction /= direction.Length();
        }
        Position += direction * delta * _speed;
    }
}

