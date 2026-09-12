using Core;

public class Player : Entity
{
    public Player()
    {
        Radius = 24f;
        Attach<PlayerController>();
        Attach<CollisionHandler>();
        Attach<PlayerDamageHandler>();
        Attach<MovementBehaviour>();
    }
}
