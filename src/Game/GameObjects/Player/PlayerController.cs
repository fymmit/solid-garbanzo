using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using Game.Traits;
using Game.GameObjects.Attacks;

namespace Game.GameObjects.Player;

internal class PlayerController(GameObject parent) : IComposable, IUpdatable, IDebugRenderable
{
    public GameObject Parent => parent;

    private float _speed = 100f;
    private Vector2 _mousePos;

    public void Update(float delta)
    {
        var movement = new Vector2();
        if (IsKeyDown(KeyboardKey.A)) movement.X += -1;
        if (IsKeyDown(KeyboardKey.D)) movement.X += 1;
        if (IsKeyDown(KeyboardKey.W)) movement.Y += -1;
        if (IsKeyDown(KeyboardKey.S)) movement.Y += 1;

        Parent.Position += movement * delta * _speed;

        _mousePos = GetScreenToWorld2D(GetMousePosition(), Program.Camera);
        if (IsMouseButtonPressed(MouseButton.Left))
        {
            Console.WriteLine(Parent.Position);
            Console.WriteLine(_mousePos);
            var direction = _mousePos - Parent.Position;
            var bullet = Program.GameObjectManager.Create<Bullet>(Parent.Position);
            bullet.SetDirection(direction);
        }

    }

    public void DebugRender()
    {
        DrawCircle((int)_mousePos.X, (int)_mousePos.Y, 12f, Color.Lime);
    }
}
