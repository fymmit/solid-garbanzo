using Core;

public class Player : Entity
{
    public Player()
    {
        Radius = 32f;
        Attach<PlayerController>();
        Attach<CollisionHandler>();
        Attach<PlayerDamageHandler>();
    }
}
