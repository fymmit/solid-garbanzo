using System.Numerics;
using Core;

internal class PlayerController : Behaviour
{
    private float _speed = 100f;

    public override void Update(float delta)
    {
        var movement = CurrentInput.Value.Movement;

        if (movement != Vector2.Zero)
        {
            movement = Vector2.Normalize(movement);
        }

        Parent.Position += movement * delta * _speed;

        if (CurrentInput.Value.FirePressed)
        {
            var direction = CurrentInput.Value.AimPosition - Parent.Position;
            var bullet = EntityManager.Create<Bullet>(Parent.Position);
            bullet.GetComponent<BulletBehaviour>()?.Direction = direction;
        }
    }
}
