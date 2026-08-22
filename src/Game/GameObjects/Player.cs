using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game.GameObjects;

internal class Player : GameObject
{
    private float _speed = 100f;

    internal override void Update(float delta)
    {
        var movement = new Vector2();
        if (IsKeyDown(KeyboardKey.A)) movement.X += -1;
        if (IsKeyDown(KeyboardKey.D)) movement.X += 1;
        if (IsKeyDown(KeyboardKey.W)) movement.Y += -1;
        if (IsKeyDown(KeyboardKey.S)) movement.Y += 1;

        Position += movement * delta * _speed;
    }
}
