using System.Numerics;
using Core;

internal class PlayerController : Behaviour
{
    private float _speed = 100f;
    private IMovable? _movementBehaviour;

    public override void Ready()
    {
        _movementBehaviour = Parent.GetComponent<IMovable>();
    }

    public override void Update(float delta)
    {
        var direction = GameInput.CurrentInput.Movement;

        if (direction != Vector2.Zero)
        {
            direction = Vector2.Normalize(direction);
        }

        var movement = direction * delta * _speed;

        _movementBehaviour?.Move(movement);
    }

    public override void DebugRender()
    {
        var mousePos = GameInput.Provider.GetMouseWorldPosition(Parent.Position);
        Gfx.Renderer.DrawLine(Parent.Position, mousePos, 0, 255, 0, 255);
    }
}
