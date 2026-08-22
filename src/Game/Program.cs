using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Game.GameObjects;

internal class Program
{
    internal static GameObjectManager GameObjectManager = new GameObjectManager();

    [STAThread]
    private static void Main(string[] args)
    {
        var windowWidth = 800;
        var windowHeight = 480;

        InitWindow(windowWidth, windowHeight, "solid-garbanzo");

        SetTargetFPS(60);

        var image = GenImageChecked(1000, 1000, 32, 32, Color.DarkGray, Color.LightGray);
        var texture = LoadTextureFromImage(image);

        Vector2 position = new((float)windowWidth / 2, (float)windowHeight / 2);
        Vector2 position2 = new((float)windowWidth / 3, (float)windowHeight / 3);
        var player = GameObjectManager.Create<Player>(position);
        GameObjectManager.Create<Player>(position2);

        Camera2D camera = new();
        camera.Target = player.Position;
        camera.Offset = new(windowWidth / 2, windowHeight / 2);
        camera.Zoom = 1f;

        while (!WindowShouldClose())
        {
            // TODO: implement some sort of game loop along the lines of:
            // Update(); -- game logic things
            // Draw(); -- draw current game state

            var delta = GetFrameTime();
            foreach (var go in GameObjectManager.GameObjects)
            {
                go.Update(delta);
            }

            camera.Target = player.Position;

            var mousePos = GetMousePosition();
            var mouseWorldPos = GetScreenToWorld2D(mousePos, camera);

            BeginDrawing();

            BeginMode2D(camera);

            ClearBackground(Color.White);
            DrawTexture(texture, 0, 0, Color.White);

            var rec1 = new Rectangle(100, 100, 100, 100);
            var rec2 = new Rectangle(150, 150, 100, 100);
            var rec3 = GetCollisionRec(rec1, rec2);
            DrawRectangle((int)rec1.X, (int)rec1.Y, (int)rec1.Width, (int)rec1.Height, Color.Red);
            DrawRectangle((int)rec2.X, (int)rec2.Y, (int)rec2.Width, (int)rec2.Height, Color.Yellow);
            DrawRectangle((int)rec3.X, (int)rec3.Y, (int)rec3.Width, (int)rec3.Height, Color.Orange);

            var radius = 32f;
            var color = Color.Blue;
            foreach (var go in GameObjectManager.GameObjects)
            {
                DrawCircle((int)go.Position.X, (int)go.Position.Y, radius, color);
            }

            EndMode2D();

            EndDrawing();
        }

        CloseWindow();
    }
}

