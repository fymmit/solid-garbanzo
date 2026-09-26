using System.Numerics;

namespace Core.Providers;

public interface IGraphicsProvider
{
    void CreateGameWindow(int width, int height);
    void StartRenderingLoop(Action<float> UpdateCallback);
    void CloseGameWindow();
    void Draw(bool isDebug, Action? DrawHud);

    void SetCameraPosition(Vector2 position);

    void LoadSprite(string filePath);
    void DrawCircle(Vector2 center, float radius, byte r, byte g, byte b, byte a, bool hasBorder);
    void DrawPoly(Vector2 center, int sides, float radius, float rotation, byte r, byte g, byte b, byte a, bool hasBorder);
    void DrawLine(Vector2 start, Vector2 end, byte r, byte g, byte b, byte a);
    void DrawSprite(string spriteId, Vector2 center, float scale, float rotation);
    void DrawRectangle(Vector2 position, Vector2 size, byte r, byte g, byte b, byte a, bool hasBorder);
    void DrawText(string text, Vector2 position, int size, byte r, byte g, byte b, byte a);

    int GetFps();
}
