using Core;
using UI;

public sealed class GameRuntime
{
    public Player Player { get; private set; } = null!;
    public HUD Hud { get; } = new();
    public GameState GameState = new(false);

    public void Initialize()
    {
        Player = EntityManager.Create<Player>();
        EntityManager.Create<EnemySpawner>();
        EntityManager.Create<Obstacle>(new(100, 100));
    }

    public void Update(float delta, GameInput input)
    {
        if (GameState.IsPaused)
        {
            return;
        }

        CurrentInput.Value = input;
        EntityManager.ProcessPendingEntities();

        var effectiveDelta = delta * GameState.TimeScale;

        foreach (var entity in EntityManager.Entities)
        {
            foreach (var component in entity.Components)
            {
                component.Update(effectiveDelta);
            }
        }
    }
}

public struct GameState(bool isPaused)
{
    public bool IsPaused = isPaused;
    public float TimeScale { get; private set; } = 1;

    public void AlterTimeScale(bool positive)
    {
        TimeScale += 0.1f * (positive ? 1 : -1);
    }
}
