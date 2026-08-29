using Core;
using Raylib_cs;

public class Player : Entity
{
    public Player()
    {
        Attach<PlayerController>();
        Attach<CollisionHandler>();
        Attach<PlayerDamageHandler>();
        Renderer = new BaseRenderer(this, Color.Blue, 32f, Shape.Circle);
    }
}

