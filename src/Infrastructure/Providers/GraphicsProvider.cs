using Core;
using Raylib_cs;
using System.Numerics;

namespace Infrastructure;

public class GraphicsProvider : IGraphicsProvider
{
    public void DrawCircle(Vector2 center, float radius, byte r, byte g, byte b, byte a, bool hasBorder)
    {
        var color = new Color(r, g, b, a);
        Raylib.DrawCircle((int)center.X, (int)center.Y, radius, color);
        if (hasBorder)
        {
            Raylib.DrawCircleLines((int)center.X, (int)center.Y, radius, Color.Black);
        }
    }

    public void DrawLine(Vector2 start, Vector2 end, byte r, byte g, byte b, byte a)
    {
        var color = new Color(r, g, b, a);
        Raylib.DrawLine((int)start.X, (int)start.Y, (int)end.X, (int)end.Y, color);
    }

    public void DrawPoly(Vector2 center, int sides, float radius, float rotation, byte r, byte g, byte b, byte a, bool hasBorder)
    {
        var color = new Color(r, g, b, a);
        Raylib.DrawPoly(center, sides, radius, rotation, color);
        if (hasBorder)
        {
            Raylib.DrawPolyLines(center, sides, radius, rotation, Color.Black);

        }
    }

    public void DrawSprite(Vector2 center, int textureId)
    {
        throw new NotImplementedException();
    }
}
