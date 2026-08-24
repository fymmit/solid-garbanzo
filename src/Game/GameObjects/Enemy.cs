using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
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

    public Enemy()
    {
        Components = [
            new Renderer(this, Color.Red, 24f, Shape.Triangle)
        ];
    }

    public override void Ready()
    {
        Console.WriteLine("Enemy ready.");
    }

    public override void Update(float delta)
    {
        Position += Vector2.UnitX * delta * _speed;
    }
}

internal class EnemyRenderer : IRenderable
{
    public Vector2 Position => _parentObject.Position;

    private GameObject _parentObject;
    private float _radius = 16f;

    internal EnemyRenderer(GameObject gameObject)
    {
        _parentObject = gameObject;
    }

    public void Render()
    {
        DrawPoly(Position, 3, _radius + 3, 0, Color.Black);
        DrawPoly(Position, 3, _radius, 0, Color.Red);
    }
}
