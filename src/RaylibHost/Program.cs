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
    private static bool _isDebug = false;

    private static List<TextureInfo> _textures = [];

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
        SetTargetFPS(150);
#endif

#if DEBUG
        _isDebug = true;
#endif

        var image = GenImageChecked(1000, 1000, 32, 32, Color.DarkGray, Color.LightGray);
        _texture = LoadTextureFromImage(image);

        foreach (var asset in Directory.EnumerateFiles(Path.Combine(AppContext.BaseDirectory, "assets")))
        {
            var texture = LoadTexture(asset);
            var width = (int)texture.Dimensions.X;
            var height = (int)texture.Dimensions.Y;
            var textureInfo = new TextureInfo
            {
                Texture = texture,
                Width = width,
                Height = height
            };
            _textures.Add(textureInfo);
        }

        Game.Initialize();

        _camera = new Camera2D
        {
            Target = Game.Player.Position,
            Offset = new Vector2(WindowWidth / 2, WindowHeight / 2),
            Zoom = 1f
        };
    }

    internal static void UpdateFrame()
    {
        Update();
        Draw();
    }

    private static void Update()
    {
        // var aimPosition = GetScreenToWorld2D(GetMousePosition(), _camera);
        Game.Update(GetFrameTime());

        if (IsKeyPressed(KeyboardKey.F3))
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

        var prioritySortedEntities = EntityManager.Entities.OrderBy(e => e.DrawPriority);
        foreach (var entity in prioritySortedEntities)
        {
            entity.Render();
        }
        if (_isDebug)
        {
            foreach (var entity in prioritySortedEntities)
            {
                entity.DebugRender();
            }
        }

        EndMode2D();
        DrawHud();
        EndDrawing();
    }

    private static void DrawHud()
    {
        DrawRectangle(10, 10, 160, 40, Color.Black);
        DrawText("HUD", 14, 14, 16, Color.White);
        DrawRectangle(8, 78, 104, 44, Color.Black);
        DrawRectangle(10, 80, Game.Hud.HealthBarPercentage, 40, Color.Red);

        DrawRectangle(396, 10, 120, 24, Color.Black);
        DrawText($"Kills: {Game.Hud.KillCount}", 400, 14, 16, Color.White);

        DrawRectangle(596, 10, 120, 24, Color.Black);
        DrawText($"FPS: {GetFPS()}", 600, 14, 16, Color.White);

        DrawRectangle(596, 40, 120, 24, Color.Black);
        DrawText($"Timescale: {Game.GameState.TimeScale.ToString("N2")}", 600, 44, 16, Color.White);

        if (_isDebug)
        {
            DrawRectangle(596, 70, 120, 24, Color.Black);
            DrawText("Debug", 600, 74, 16, Color.White);

        }

        // TODO: rethink cooldown drawing with the new Timers implementation

        // var skillbar = Game.Player.GetComponent<Skillbar>();
        // if (skillbar is not null)
        // {
        //     for (var i = 0; i < skillbar.Skills.Count; i++)
        //     {
        //         var skill = skillbar.Skills[i];
        //         var cd = skill.GetComponent<Cooldown>();
        //         if (cd is not null)
        //         {
        //             var text = cd.IsReady() ? "Ready" : cd.RemainingCooldown.ToString("N1");
        //             var x = i * 140 + 10;
        //             DrawRectangle(x, GetScreenHeight() - 50, 120, 30, Color.Black);
        //             DrawText(text, x + 4, GetScreenHeight() - 46, 24, Color.White);
        //         }
        //     }
        // }
    }
}

internal struct TextureInfo
{
    internal Texture2D Texture;
    internal int Width;
    internal int Height;
}
