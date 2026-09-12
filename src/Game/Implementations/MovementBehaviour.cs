using System.Numerics;
using Core;

public class MovementBehaviour : Behaviour, IMovable
{
    public void Move(Vector2 movement)
    {
        var newPosition = Parent.Position + movement;

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
    }
}
