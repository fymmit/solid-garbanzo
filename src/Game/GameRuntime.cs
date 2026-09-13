using Core;
using UI;

public sealed class GameRuntime
{
    public Player Player { get; private set; } = null!;
    public HUD Hud { get; } = new();

    public void Initialize()
    {
        Player = EntityManager.Create<Player>();
        EntityManager.Create<EnemySpawner>();
        EntityManager.Create<Obstacle>(new(100, 100));
    }

    public void Update(float delta, GameInput input)
    {
        CurrentInput.Value = input;
        EntityManager.ProcessPendingEntities();

        foreach (var entity in EntityManager.Entities)
        {
            foreach (var component in entity.Components)
            {
                component.Update(delta);
            }
        }
    }
}
