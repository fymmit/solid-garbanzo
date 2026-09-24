using Core;
using UI;

public sealed class GameRuntime
{
    public Player Player { get; private set; } = null!;
    public HUD Hud { get; } = new();
    public GameState GameState = new();

    public void Initialize()
    {
        Player = EntityManager.Create<Player>();
        EntityManager.Create<EnemySpawner>();
        EntityManager.Create<Obstacle>(new(100, 100));
    }

    public void Update(float delta)
    {
        GameInput.GetInput();

        if (GameInput.CurrentInput.PausePressed)
        {
            GameState.IsPaused = !GameState.IsPaused;
        }

        if (GameInput.CurrentInput.TimescaleDecreasePressed)
        {
            GameState.AlterTimeScale(false);
        }

        if (GameInput.CurrentInput.TimescaleIncreasePressed)
        {
            GameState.AlterTimeScale(true);
        }

        if (GameState.IsPaused)
        {
            return;
        }

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

public struct GameState()
{
    public bool IsPaused { get; internal set; } = false;
    public float TimeScale { get; private set; } = 1;

    internal void AlterTimeScale(bool positive)
    {
        TimeScale += 0.1f * (positive ? 1 : -1);
    }
}
