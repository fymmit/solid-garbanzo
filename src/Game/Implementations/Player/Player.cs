using Core;

public class Player : Entity
{
    private const string SPRITE_ID = "player.png";

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
        Gfx.Renderer.DrawSprite(SPRITE_ID, Position, 1f, 0f);
    }
}
