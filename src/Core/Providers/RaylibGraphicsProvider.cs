using Raylib_cs;
using System.Numerics;

namespace Core.Providers;

public class RaylibGraphicsProvider : IGraphicsProvider
{
    private Camera2D _camera;
    private List<SpriteInfo> _sprites = new();

    public void CreateGameWindow(int width, int height)
    {
        Raylib.InitWindow(width, height, "solid-garbanzo");

#if !BROWSER_WASM
        Raylib.SetTargetFPS(150);
#endif

        _camera = new Camera2D
        {
            Offset = new Vector2(width / 2, height / 2),
            Zoom = 1f
        };
    }

    public void StartRenderingLoop(Action<float> UpdateCallback)
    {
        while (!Raylib.WindowShouldClose())
        {
            UpdateCallback(Raylib.GetFrameTime());
        }
    }

    public void CloseGameWindow()
    {
        Raylib.CloseWindow();
    }

    public void Draw(bool isDebug, Action? DrawHud)
    {
        Raylib.BeginDrawing();
        Raylib.BeginMode2D(_camera);

        Raylib.ClearBackground(Color.White);

        var prioritySortedEntities = EntityManager.Entities.OrderBy(e => e.DrawPriority);
        foreach (var entity in prioritySortedEntities)
        {
            entity.Render();
        }

        if (isDebug)
        {
            foreach (var entity in prioritySortedEntities)
            {
                entity.DebugRender();
            }
        }

        Raylib.EndMode2D();

        if (DrawHud is not null)
        {
            DrawHud();
        }

        Raylib.EndDrawing();
    }

    public void SetCameraPosition(Vector2 position)
    {
        _camera.Target = position;
    }

    public void LoadSprite(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var texture = Raylib.LoadTexture(filePath);
        var width = (int)texture.Dimensions.X;
        var height = (int)texture.Dimensions.Y;
        var spriteInfo = new SpriteInfo
        {
            SpriteId = fileName,
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

    public void DrawSprite(string spriteId, Vector2 center, float scale, float rotation)
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

    public void DrawRectangle(Vector2 position, Vector2 size, byte r, byte g, byte b, byte a, bool hasBorder)
    {
        var color = new Color(r, g, b, a);
        Raylib.DrawRectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, color);
    }

    public void DrawText(string text, Vector2 position, int size, byte r, byte g, byte b, byte a)
    {
        var color = new Color(r, g, b, a);
        Raylib.DrawText(text, (int)position.X, (int)position.Y, size, color);
    }

    public int GetFps() => Raylib.GetFPS();
}

internal record SpriteInfo
{
    internal required string SpriteId;
    internal required Texture2D Texture;
    internal required int Width;
    internal required int Height;
}
