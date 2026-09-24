using Core;
using Infrastructure;
using UI;

public sealed class GameRuntime
{
    public Player Player { get; private set; } = null!;
    public HUD Hud { get; } = new();
    public GameState GameState = new();
    private IInputProvider _inputProvider = new InputProvider();
    internal static IGraphicsProvider Gfx = new GraphicsProvider();

    public void Initialize()
    {
        Player = EntityManager.Create<Player>();
        EntityManager.Create<EnemySpawner>();
        EntityManager.Create<Obstacle>(new(100, 100));
    }

    public void Update(float delta)
    {
        CurrentInput.Value = _inputProvider.GetInput();

        if (CurrentInput.Value.PausePressed)
        {
            GameState.IsPaused = !GameState.IsPaused;
        }

        if (CurrentInput.Value.TimescaleDecreasePressed)
        {
            GameState.AlterTimeScale(false);
        }

        if (CurrentInput.Value.TimescaleIncreasePressed)
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
