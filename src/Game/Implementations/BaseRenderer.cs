using Raylib_cs;
using Core;

public class BaseRenderer : Renderer
{
    private Color _color;
    private float _radius;
    private Shape _shape;

    public BaseRenderer(Entity parent, Color color, float radius, Shape shape)
    {
        Parent = parent;
        _color = color;
        _radius = radius;
        _shape = shape;
    }

    public override void Render()
    {
        if (_shape == Shape.Circle)
        {
            Raylib.DrawCircle((int)Parent.Position.X, (int)Parent.Position.Y, _radius, _color);
        }
        else if (_shape == Shape.Triangle)
        {
            Raylib.DrawPoly(Parent.Position, 3, _radius, 0, _color);
        }
    }
}

public enum Shape
{
    Circle,
    Triangle
}
