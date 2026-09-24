using Core;
using Raylib_cs;
using System.Numerics;

namespace Infrastructure;

public class InputProvider : IInputProvider
{
    public Input GetInput()
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

        return new Input
        {
            Movement = movement,
            PausePressed = Raylib.IsKeyPressed(KeyboardKey.P),
            TimescaleDecreasePressed = Raylib.IsKeyPressed(KeyboardKey.Nine),
            TimescaleIncreasePressed = Raylib.IsKeyPressed(KeyboardKey.Zero)
        };
    }
}
