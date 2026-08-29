using Core;

public class Player : Entity
{
    public Player()
    {
        Attach<PlayerController>();
        Attach<CollisionHandler>();
        Attach<PlayerDamageHandler>();
    }
}
