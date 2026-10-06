using Raylib_cs;
using System.Numerics;

namespace Core.Providers;

public class RaylibInputProvider : IInputProvider
{
    public InputState GetInput()
    {
        var movement = new Vector2();
        if (Raylib.IsKeyDown(KeyboardKey.A)) movement.X += -1;
        if (Raylib.IsKeyDown(KeyboardKey.D)) movement.X += 1;
        if (Raylib.IsKeyDown(KeyboardKey.W)) movement.Y += -1;
        if (Raylib.IsKeyDown(KeyboardKey.S)) movement.Y += 1;

        if (movement != Vector2.Zero)
        {
            movement = Vector2.Normalize(movement);
        }

        return new InputState
        {
            Movement = movement,
            DebugTogglePressed = Raylib.IsKeyPressed(KeyboardKey.O),
            PausePressed = Raylib.IsKeyPressed(KeyboardKey.P),
            TimescaleDecreasePressed = Raylib.IsKeyPressed(KeyboardKey.Nine),
            TimescaleIncreasePressed = Raylib.IsKeyPressed(KeyboardKey.Zero),
            MouseScreenPosition = GetMouseScreenPosition(),
            MouseWorldPosition = GetMouseWorldPosition(Gfx.Provider.CameraPosition),
            MouseLeftClicked = Raylib.IsMouseButtonPressed(MouseButton.Left)
        };
    }

    private Vector2 GetMouseScreenPosition()
    {
        return Raylib.GetMousePosition();
    }

    private Vector2 GetMouseWorldPosition(Vector2 offset)
    {
        var screenCenter = Raylib.GetScreenCenter();
        var mouseScreenPosition = GetMouseScreenPosition();
        return offset + mouseScreenPosition - screenCenter;
    }
}
