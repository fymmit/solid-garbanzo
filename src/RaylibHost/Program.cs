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

        if (IsKeyPressed(KeyboardKey.X))
        {
            Game.GameState.IsPaused = !Game.GameState.IsPaused;
        }

        if (IsKeyPressed(KeyboardKey.Nine))
        {
            Game.GameState.AlterTimeScale(false);
        }
        if (IsKeyPressed(KeyboardKey.Zero))
        {
            Game.GameState.AlterTimeScale(true);
        }

        _camera.Target = Game.Player.Position;
    }

    private static void Draw()
    {
        BeginDrawing();
        BeginMode2D(_camera);

        ClearBackground(Color.White);
        DrawTexture(_texture, 0, 0, Color.White);

        foreach (var entity in EntityManager.Entities)
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
                var texture = _textures[0];
                var x = (int)entity.Position.X - texture.Width / 2;
                var y = (int)entity.Position.Y - texture.Height / 2;
                DrawTexture(texture.Texture, x, y, Color.White);
                if (_isDebug)
                {
                    DrawCircleLines((int)entity.Position.X, (int)entity.Position.Y, entity.Radius, Color.Green);
                }
                break;
            case Obstacle:
                DrawCircle((int)entity.Position.X, (int)entity.Position.Y, entity.Radius, Color.Pink);
                break;
            case Enemy:
                DrawPoly(entity.Position, 3, entity.Radius, entity.Rotation, Color.Red);
                break;
            case Bullet:
                DrawCircle((int)entity.Position.X, (int)entity.Position.Y, entity.Radius, Color.Black);
                break;
            case SpiderWeb:
                DrawCircle((int)entity.Position.X, (int)entity.Position.Y, entity.Radius, Color.Gray);
                break;
            case BloodParticle:
                // TODO: rethink the following logic with the new Timers implementation

                // var duration = entity.GetComponent<Duration>();
                // if (duration is not null)
                // {
                //     DrawPoly(
                //         entity.Position,
                //         6,
                //         32f,
                //         0,
                //         new Color(
                //             255,
                //             0,
                //             0,
                //             (duration.RemainingLifeTime / duration.OriginalLifeTime)));
                // }
                DrawPoly(entity.Position, 6, 32f, 0, Color.Red);
                break;
        }
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
