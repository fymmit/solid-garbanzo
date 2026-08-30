using System.Numerics;
using Core;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace RaylibHost;

internal static class Program
{
    private const int WindowWidth = 800;
    private const int WindowHeight = 480;

    private static readonly GameRuntime Game = new();
    private static Camera2D _camera;
    private static Texture2D _texture;
    private static bool _isDebug;

    [STAThread]
    private static void Main()
    {
        Initialize();

        while (!WindowShouldClose())
        {
            UpdateFrame();
        }

        CloseWindow();
    }

    internal static void Initialize()
    {
        InitWindow(WindowWidth, WindowHeight, "solid-garbanzo");

#if !BROWSER_WASM
        SetTargetFPS(60);
#endif

        var image = GenImageChecked(1000, 1000, 32, 32, Color.DarkGray, Color.LightGray);
        _texture = LoadTextureFromImage(image);

        Game.Initialize(new Vector2((float)WindowWidth / 2, (float)WindowHeight / 2));

        _camera = new Camera2D
        {
            Target = Game.Player.Position,
            Offset = new Vector2(WindowWidth / 2, WindowHeight / 2),
            Zoom = 1f
        };

        _isDebug = false;
    }

    internal static void UpdateFrame()
    {
        Update();
        Draw();
    }

    private static void Update()
    {
        var movement = new Vector2();
        if (IsKeyDown(KeyboardKey.A)) movement.X += -1;
        if (IsKeyDown(KeyboardKey.D)) movement.X += 1;
        if (IsKeyDown(KeyboardKey.W)) movement.Y += -1;
        if (IsKeyDown(KeyboardKey.S)) movement.Y += 1;

        var aimPosition = GetScreenToWorld2D(GetMousePosition(), _camera);
        var input = new GameInput(movement, aimPosition, IsMouseButtonPressed(MouseButton.Left));
        Game.Update(GetFrameTime(), input);

        if (IsKeyPressed(KeyboardKey.P))
        {
            _isDebug = !_isDebug;
        }

        _camera.Target = Game.Player.Position;
    }

    private static void Draw()
    {
        BeginDrawing();
        BeginMode2D(_camera);

        ClearBackground(Color.White);
        DrawTexture(_texture, 0, 0, Color.White);

        foreach (var entity in Game.Entities)
        {
            DrawEntity(entity);
        }

        EndMode2D();
        DrawHud();
        EndDrawing();
    }

    private static void DrawEntity(Entity entity)
    {
        switch (entity)
        {
            case Player:
                DrawCircle((int)entity.Position.X, (int)entity.Position.Y, 32f, Color.Blue);
                break;
            case Enemy:
                DrawPoly(entity.Position, 3, 24f, 0, Color.Red);
                break;
            case Bullet:
                DrawCircle((int)entity.Position.X, (int)entity.Position.Y, 12f, Color.Magenta);
                break;
        }
    }

    private static void DrawHud()
    {
        DrawRectangle(10, 10, 160, 40, Color.Black);
        DrawText("HUD", 14, 14, 16, Color.White);
        DrawRectangle(8, 78, 104, 44, Color.Black);
        DrawRectangle(10, 80, Game.Hud.HealthBarPercentage, 40, Color.Red);
    }
}
