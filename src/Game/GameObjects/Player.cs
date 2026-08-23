using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using Game.GameObjects.Traits;
using Game.Events;

namespace Game.GameObjects;

internal class Player : GameObject
{
    private float _speed = 100f;

    private Color _color = Color.Blue;
    private float _radius = 32f;

    internal override void Update(float delta)
    {
        var movement = new Vector2();
        if (IsKeyDown(KeyboardKey.A)) movement.X += -1;
        if (IsKeyDown(KeyboardKey.D)) movement.X += 1;
        if (IsKeyDown(KeyboardKey.W)) movement.Y += -1;
        if (IsKeyDown(KeyboardKey.S)) movement.Y += 1;

        Position += movement * delta * _speed;

        var harmfuls = Program.GameObjectManager.GameObjects.Where(go => go is IHarmful);
        foreach (var go in harmfuls)
        {
            var harmful = (IHarmful)go;
            if (CheckCollisionCircles(Position, _radius, harmful.ColliderPosition, harmful.ColliderRadius))
            {
                DamageEventChannel.InvokeDamageEvent(harmful.Damage);
            }
        }
    }

    internal override void Draw()
    {
        DrawCircle((int)Position.X, (int)Position.Y, _radius, _color);
    }
}
