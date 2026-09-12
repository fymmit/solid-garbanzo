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

        var newPosition = Parent.Position + movement * delta * _speed;

        var obstacles = _entityManager.Find<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            var wouldBlock = Geometry.CheckCollisionCircles(newPosition, Parent.Radius, obstacle.Position, obstacle.Radius);
            if (wouldBlock)
            {
                var difference = newPosition - obstacle.Position;
                newPosition += Vector2.Normalize(difference);
            }
        }

        Parent.Position = newPosition;


        if (CurrentInput.Value.FirePressed)
        {
            var direction = CurrentInput.Value.AimPosition - Parent.Position;
            var bullet = _entityManager.Create<Bullet>(Parent.Position);
            bullet.GetComponent<BulletBehaviour>()?.Direction = direction;
        }
    }
}
