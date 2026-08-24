using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using Game.GameObjects.Traits;

namespace Game.GameObjects.Player;

internal class PlayerController(GameObject parent) : IComposable, IUpdatable
{
    public GameObject Parent => parent;

    private float _speed = 100f;

    public void Update(float delta)
    {
        var movement = new Vector2();
        if (IsKeyDown(KeyboardKey.A)) movement.X += -1;
        if (IsKeyDown(KeyboardKey.D)) movement.X += 1;
        if (IsKeyDown(KeyboardKey.W)) movement.Y += -1;
        if (IsKeyDown(KeyboardKey.S)) movement.Y += 1;

        Parent.Position += movement * delta * _speed;
    }
}


