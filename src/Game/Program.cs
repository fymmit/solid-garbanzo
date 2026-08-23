using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Game.GameObjects;
using Game.Events;

internal class Program
{
    internal static GameObjectManager GameObjectManager = new GameObjectManager();
    private static Camera2D _camera;
    private static Texture2D _texture;
    private static Player? _player;

    [STAThread]
    private static void Main(string[] args)
    {
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

        _camera = new();
        _camera.Target = _player.Position;
        _camera.Offset = new(windowWidth / 2, windowHeight / 2);
        _camera.Zoom = 1f;

        while (!WindowShouldClose())
        {
            Update();
            Draw();
        }

        CloseWindow();
    }

    private static void Update()
    {
        var delta = GetFrameTime();
        foreach (var go in GameObjectManager.GameObjects)
        {
            go.Update(delta);
        }

        _camera.Target = _player?.Position ?? new();
    }

    private static void Draw()
    {
        BeginDrawing();

        BeginMode2D(_camera);

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
            go.Draw();
        }

        EndMode2D();

        EndDrawing();
    }

    private static void OnDamage(int damage)
    {
        Console.WriteLine($"OnDamage: {damage}");
    }
}

