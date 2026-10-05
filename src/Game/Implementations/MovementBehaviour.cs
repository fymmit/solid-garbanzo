using System.Numerics;
using Core;

public class MovementBehaviour : Behaviour, IMovable
{
    public float MovementSpeedCoefficient = 1f;

    public void Move(Vector2 movement)
    {
        if (movement == Vector2.Zero)
        {
            return;
        }

        var newPosition = Parent.Position + movement * MovementSpeedCoefficient;

        var obstacles = EntityManager.Find<Obstacle>();

        // var collisionPoint = newPosition + Vector2.Normalize(movement) * Parent.Radius;

        foreach (var obstacle in obstacles)
        {
            var wouldBlock = Geometry.CheckCollision(Parent, obstacle);
            if (wouldBlock)
            {
                // FIX: this logic is dog, only works for circle to circle collision
                var direction = Vector2.Normalize(Parent.Position - obstacle.Position);
                var distance = Vector2.Distance(Parent.Position, obstacle.Position) - Parent.Radius - obstacle.Radius;
                newPosition -= direction * distance;
                // newPosition = Parent.Position;
            }
        }

        Parent.Position = newPosition;
    }
}
