using System.Numerics;
using Core;

public class Wall : Obstacle
{
    public Wall()
    {
        Radius = 32f;
        Shape = Shape.Square;
        Attach<WallBehaviour>();
    }

    public override void Render()
    {
        Vector2 pos = new(Position.X - Radius, Position.Y - Radius);
        Gfx.Provider.DrawRectangle(pos, new(Radius * 2), 125, 50, 25, 255, true);
    }
}

public class WallBehaviour : Behaviour
{
    public override void DebugRender()
    {
        Gfx.Provider.DrawCircle(Parent.Position, 4f, 255, 255, 0, 255, false);
    }
}
