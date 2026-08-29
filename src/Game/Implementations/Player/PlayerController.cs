using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using Core;

internal class PlayerController : Behaviour
{
    private float _speed = 100f;

    public override void Update(float delta)
    {
        var movement = new Vector2();
        if (IsKeyDown(KeyboardKey.A)) movement.X += -1;
        if (IsKeyDown(KeyboardKey.D)) movement.X += 1;
        if (IsKeyDown(KeyboardKey.W)) movement.Y += -1;
        if (IsKeyDown(KeyboardKey.S)) movement.Y += 1;

        if (movement != Vector2.Zero)
        {
            movement = Vector2.Normalize(movement);
        }

        Parent.Position += movement * delta * _speed;

        var mousePos = GetScreenToWorld2D(GetMousePosition(), Program.Camera);
        if (IsMouseButtonPressed(MouseButton.Left))
        {
            var direction = mousePos - Parent.Position;
            var bullet = EntityManager.Create<Bullet>(Parent.Position);
            bullet.GetComponent<BulletBehaviour>()?.Direction = direction;
        }
    }
}
