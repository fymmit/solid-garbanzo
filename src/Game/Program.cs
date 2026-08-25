using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Game.GameObjects;
using Game.GameObjects.Player;
using Game.Events;
using Game.Traits;
using Game.UI;

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
        GameObjectManager.Setup();
        DamageEventChannel.DamageEvent += OnDamage;

        var windowWidth = 800;
        var windowHeight = 480;

        InitWindow(windowWidth, windowHeight, "solid-garbanzo");

        SetTargetFPS(60);

        var image = GenImageChecked(1000, 1000, 32, 32, Color.DarkGray, Color.LightGray);
        _texture = LoadTextureFromImage(image);

        Vector2 position = new((float)windowWidth / 2, (float)windowHeight / 2);
        Vector2 position2 = new((float)windowWidth / 3, (float)windowHeight / 3);
        _player = GameObjectManager.Create<Player>(position);
        GameObjectManager.Create<Enemy>(position2);

        Camera = new();
        Camera.Target = _player.Position;
        Camera.Offset = new(windowWidth / 2, windowHeight / 2);
        Camera.Zoom = 1f;

        _isDebug = false;

        while (!WindowShouldClose())
        {
            Update();
            Draw();
        }

        CloseWindow();
    }

    private static void Update()
    {
        UpdateEventChannel.InvokeUpdateLoopStartedEvent();

        var delta = GetFrameTime();
        foreach (var go in GameObjectManager.GameObjects)
        {
            go.Update(delta);
            foreach (var updatable in go.GetComponents<IUpdatable>())
            {
                updatable.Update(delta);
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

        foreach (var go in GameObjectManager.GameObjects)
        {
            foreach (var renderable in go.GetComponents<IRenderable>())
            {
                renderable.Render();
            }
        }

        if (_isDebug)
        {
            foreach (var go in GameObjectManager.GameObjects)
            {
                go.DebugRender();
                foreach (var debugRenderable in go.GetComponents<IDebugRenderable>())
                {
                    debugRenderable.DebugRender();
                }
            }
        }

        _hud?.Render();

        EndMode2D();

        EndDrawing();
    }

    private static void OnDamage(int damage)
    {
        Console.WriteLine($"OnDamage: {damage}");
    }
}

