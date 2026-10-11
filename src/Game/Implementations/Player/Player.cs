using Core;
using Events;

public class Player : Entity
{
    public uint Experience { get; private set; } = 0;
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

        DeathEventChannel.DeathEvent += OnDeath;
    }

    public override void Render()
    {
        Gfx.Provider.DrawSprite(SPRITE_ID, Position, 1f, 0f);
    }

    private void OnDeath(Entity? entity)
    {
        if (entity is Enemy)
        {
            Experience += 1;
        }
    }
}
