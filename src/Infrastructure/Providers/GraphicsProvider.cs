using Core;
using Raylib_cs;
using System.Numerics;

namespace Infrastructure;

public class GraphicsProvider : IGraphicsProvider
{
    private List<SpriteInfo> _sprites = new();

    public void LoadSprite(string filePath)
    {
        var texture = Raylib.LoadTexture(filePath);
        var width = (int)texture.Dimensions.X;
        var height = (int)texture.Dimensions.Y;
        var spriteInfo = new SpriteInfo
        {
            // TODO: spriteId from somewhere. maybe just use file name as the id?
            Texture = texture,
            Width = width,
            Height = height
        };

        _sprites.Add(spriteInfo);
    }

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

    public void DrawSprite(int spriteId, Vector2 center, float scale, float rotation)
    {
        SpriteInfo? spriteInfo = _sprites.SingleOrDefault(t => t.SpriteId == spriteId);

        if (spriteInfo is null)
        {
            return;
        }

        var texture = spriteInfo.Texture;
        var x = (int)center.X - texture.Width / 2;
        var y = (int)center.Y - texture.Height / 2;
        Raylib.DrawTexture(texture, x, y, Color.White);
    }
}

internal record SpriteInfo
{
    internal int SpriteId;
    internal Texture2D Texture;
    internal int Width;
    internal int Height;
}
