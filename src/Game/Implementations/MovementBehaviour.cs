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

        foreach (var obstacle in obstacles)
        {
            var direction = Vector2.Normalize(Parent.Position - obstacle.Position);
            var wouldBlock = Geometry.CheckCollision(Parent, obstacle);
            if (wouldBlock)
            {
                if (Math.Abs(direction.X) > Math.Abs(direction.Y))
                {
                    direction = new Vector2(direction.X, 0);
                }
                else
                {
                    direction = new Vector2(0, direction.Y);
                }
                newPosition = Parent.Position + direction;
            }
        }

        Parent.Position = newPosition;
    }
}
