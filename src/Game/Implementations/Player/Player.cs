using Core;

public class Player : Entity
{
    public Player()
    {
        DrawPriority = 10;
        Radius = 24f;
        Attach<PlayerController>();
        Attach<CollisionHandler>();
        Attach<PlayerDamageHandler>();
        Attach<MovementBehaviour>();
        Attach<AutoTargeting>();
        Attach<Skillbar>();
    }

    public override void Render()
    {
        Gfx.Renderer.DrawCircle(Position, Radius, 50, 200, 255, 255, true);
    }
}
