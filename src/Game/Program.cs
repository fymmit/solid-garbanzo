using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using UI;
using Core;
using Core.Events;

internal class Program
{
    internal static Camera2D Camera;
    private static Texture2D _texture;
    private static Player? _player;
    private static HUD _hud = new();

    private static bool _isDebug;

    [STAThread]
    private static void Main(string[] args)
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
        EntityManager.Setup();

        var windowWidth = 800;
        var windowHeight = 480;

        InitWindow(windowWidth, windowHeight, "solid-garbanzo");

#if !BROWSER_WASM
        SetTargetFPS(60);
#endif

        var image = GenImageChecked(1000, 1000, 32, 32, Color.DarkGray, Color.LightGray);
        _texture = LoadTextureFromImage(image);

        Vector2 position = new((float)windowWidth / 2, (float)windowHeight / 2);
        _player = EntityManager.Create<Player>(position);

        EntityManager.Create<EnemySpawner>();

        Camera = new();
        Camera.Target = _player.Position;
        Camera.Offset = new(windowWidth / 2, windowHeight / 2);
        Camera.Zoom = 1f;

        _isDebug = false;
    }

    internal static void UpdateFrame()
    {
        Update();
        Draw();
    }

    private static void Update()
    {
        UpdateEventChannel.InvokeUpdateLoopStartedEvent();

        var delta = GetFrameTime();

        foreach (var entity in EntityManager.Entities)
        {
            foreach (var component in entity.Components)
            {
                component.Update(delta);
            }
        }

        if (IsKeyPressed(KeyboardKey.P))
        {
            _isDebug = !_isDebug;
        }

        Camera.Target = _player?.Position ?? new();

        UpdateEventChannel.InvokeUpdateLoopFinishedEvent();
    }

    private static void Draw()
    {
        BeginDrawing();

        BeginMode2D(Camera);

        ClearBackground(Color.White);
        DrawTexture(_texture, 0, 0, Color.White);

        var rec1 = new Rectangle(100, 100, 100, 100);
        var rec2 = new Rectangle(150, 150, 100, 100);
        var rec3 = GetCollisionRec(rec1, rec2);
        DrawRectangle((int)rec1.X, (int)rec1.Y, (int)rec1.Width, (int)rec1.Height, Color.Red);
        DrawRectangle((int)rec2.X, (int)rec2.Y, (int)rec2.Width, (int)rec2.Height, Color.Yellow);
        DrawRectangle((int)rec3.X, (int)rec3.Y, (int)rec3.Width, (int)rec3.Height, Color.Orange);

        foreach (var go in EntityManager.Entities)
        {
            go.Renderer?.Render();
        }

        // if (_isDebug)
        // {
        //     foreach (var go in EntityManager.Entities)
        //     {
        //         go.DebugRender();
        //         foreach (var debugRenderable in go.GetComponents<IDebugRenderable>())
        //         {
        //             debugRenderable.DebugRender();
        //         }
        //     }
        // }

        EndMode2D();

        _hud?.Render();

        EndDrawing();
    }
}
