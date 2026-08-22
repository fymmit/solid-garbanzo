using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var windowWidth = 800;
        var windowHeight = 480;

        InitWindow(windowWidth, windowHeight, "solid-garbanzo");

        Vector2 position = new((float)windowWidth / 2, (float)windowHeight / 2);
        Camera2D camera = new();
        camera.Target = position;
        camera.Offset = new(windowWidth / 2, windowHeight / 2);
        camera.Zoom = 1f;

        SetTargetFPS(60);

        var speed = 100f;

        var image = GenImageChecked(1000, 1000, 32, 32, Color.DarkGray, Color.LightGray);
        var texture = LoadTextureFromImage(image);

        while (!WindowShouldClose())
        {
            var movement = new Vector2();
            if (IsKeyDown(KeyboardKey.A)) movement.X += -1;
            if (IsKeyDown(KeyboardKey.D)) movement.X += 1;
            if (IsKeyDown(KeyboardKey.W)) movement.Y += -1;
            if (IsKeyDown(KeyboardKey.S)) movement.Y += 1;

            position += movement * GetFrameTime() * speed;

            camera.Target = position;

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
            if (CheckCollisionPointCircle(mouseWorldPos, position, radius))
            {
                color = Color.DarkBlue;
            }

            DrawCircle((int)position.X, (int)position.Y, radius, color);

            EndMode2D();

            EndDrawing();
        }

        CloseWindow();
    }
}

