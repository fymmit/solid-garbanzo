using System.Numerics;
using Raylib_cs;
using Game.Traits;

namespace Game.GameObjects;

public class Renderer : IComposable, IRenderable
{
    public GameObject Parent => _parent;
    public Vector2 Position => _parent.Position;

    private GameObject _parent;
    private Color _color;
    private float _radius;
    private Shape _shape;

    public Renderer(GameObject parent, Color color, float radius, Shape shape)
    {
        _parent = parent;
        _color = color;
        _radius = radius;
        _shape = shape;
    }

    public void Render()
    {
        if (_shape == Shape.Circle)
        {
            Raylib.DrawCircle((int)Position.X, (int)Position.Y, _radius, _color);
        }
        else if (_shape == Shape.Triangle)
        {
            Raylib.DrawPoly(Position, 3, _radius, 0, _color);
        }
    }
}

public enum Shape
{
    Circle,
    Triangle
}
