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
        var direction = CurrentInput.Value.Movement;

        if (direction != Vector2.Zero)
        {
            direction = Vector2.Normalize(direction);
        }

        var movement = direction * delta * _speed;

        _movementBehaviour?.Move(movement);

        if (CurrentInput.Value.FirePressed)
        {
            var aimDirection = CurrentInput.Value.AimPosition - Parent.Position;
            var bullet = _entityManager.Create<Bullet>(Parent.Position);
            bullet.GetComponent<BulletBehaviour>()?.Direction = aimDirection;
        }
    }
}
