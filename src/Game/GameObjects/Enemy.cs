using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Game.GameObjects.Traits;

namespace Game.GameObjects;

internal class Enemy : GameObject, IHarmful
{
    private float _speed = 10f;

    private Color _color = Color.Red;
    private float _radius = 16f;

    public Vector2 ColliderPosition => Position;
    public float ColliderRadius => _radius;
    public int Damage => 5;

    internal override void Ready()
    {
        Console.WriteLine("Enemy created");
    }

    internal override void Update(float delta)
    {
        Position += Vector2.UnitX * delta * _speed;
    }

    internal override void Draw()
    {
        DrawPoly(Position, 3, _radius, 0, _color);
    }
}

