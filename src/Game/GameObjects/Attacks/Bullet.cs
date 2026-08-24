using System.Numerics;

namespace Game.GameObjects.Attacks;

public class Bullet : GameObject
{
    private Vector2 _direction;
    private float _speed = 50f;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction / direction.Length();
    }

    public Bullet()
    {
        Components = [
            new Renderer(this, Raylib_cs.Color.Magenta, 12f, Shape.Circle)
        ];
    }

    public override void Ready()
    {
        Console.WriteLine(_direction);
    }

    public override void Update(float delta)
    {
        Position += _direction * _speed * delta;
    }
}
