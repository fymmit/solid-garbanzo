using Core;
using UI;

public sealed class GameRuntime
{
    public Player Player { get; private set; } = null!;
    public HUD Hud { get; } = new();
    private GameState _state = new();

    public void Run(bool isSimulation)
    {

#if DEBUG
        _state.IsDebug = true;
#endif

        Gfx.Renderer.CreateGameWindow(800, 480);

        foreach (var asset in Directory.EnumerateFiles(Path.Combine(AppContext.BaseDirectory, "Assets")))
        {
            Gfx.Renderer.LoadSprite(asset);
        }

        Player = EntityManager.Create<Player>();
        EntityManager.Create<EnemySpawner>();
        EntityManager.Create<Obstacle>(new(100, 100));

        if (!isSimulation)
        {
            Gfx.Renderer.StartRenderingLoop((float delta) =>
            {
                UpdateEntities(delta);
                Gfx.Renderer.SetCameraPosition(Player.Position);
                Gfx.Renderer.Draw(_state.IsDebug);
            });
        }
        else
        {
            var frameTime = 0.1f;

            var simulationDuration = 60f;

            var steps = simulationDuration / frameTime;

            for (var i = 0; i < steps; i++)
            {
                UpdateEntities(frameTime);
            }

        }
    }

    private void UpdateEntities(float delta)
    {
        Input.GetInput(Player.Position);

        if (Input.Current.DebugTogglePressed)
        {
            _state.IsDebug = !_state.IsDebug;
        }

        if (Input.Current.PausePressed)
        {
            _state.IsPaused = !_state.IsPaused;
        }

        if (Input.Current.TimescaleDecreasePressed)
        {
            _state.AlterTimeScale(false);
        }

        if (Input.Current.TimescaleIncreasePressed)
        {
            _state.AlterTimeScale(true);
        }

        if (_state.IsPaused)
        {
            return;
        }

        EntityManager.ProcessPendingEntities();

        var effectiveDelta = delta * _state.TimeScale;

        foreach (var entity in EntityManager.Entities)
        {
            foreach (var component in entity.Components)
            {
                component.Update(effectiveDelta);
            }
        }
    }
}

struct GameState()
{
    public bool IsDebug { get; internal set; } = false;
    public bool IsPaused { get; internal set; } = false;
    public float TimeScale { get; private set; } = 1;

    internal void AlterTimeScale(bool positive)
    {
        TimeScale += 0.1f * (positive ? 1 : -1);
    }
}
