using System.Numerics;
using Core;
using Core.Events;
using UI;

public sealed class GameRuntime
{
    public Player Player { get; private set; } = null!;
    public HUD Hud { get; } = new();

    public void Initialize(Vector2 playerPosition)
    {
        EntityManager.Setup();
        Player = EntityManager.Create<Player>(playerPosition);
        EntityManager.Create<EnemySpawner>();
    }

    public void Update(float delta, GameInput input)
    {
        CurrentInput.Value = input;
        UpdateEventChannel.InvokeUpdateLoopStartedEvent();

        foreach (var entity in EntityManager.Entities)
        {
            foreach (var component in entity.Components)
            {
                component.Update(delta);
            }
        }

        UpdateEventChannel.InvokeUpdateLoopFinishedEvent();
    }
}
