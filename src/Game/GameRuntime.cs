using Core;
using UIElements;

public sealed class GameRuntime
{
    public Player Player { get; private set; } = null!;
    private HUD? _hud;
    private MainMenu _mainMenu = new();
    private PauseMenu _pauseMenu = new();
    private GameState _state = new();

    public void Initialize()
    {
#if DEBUG
        _state.IsDebug = true;
#endif

        Gfx.Provider.CreateGameWindow(800, 480);

        foreach (var asset in Directory.EnumerateFiles(Path.Combine(AppContext.BaseDirectory, "Assets")))
        {
            Gfx.Provider.LoadSprite(asset);
        }

        Player = EntityManager.Create<Player>();
        _hud = new(Player, _state);
        EntityManager.Create<EnemySpawner>();
        EntityManager.Create<Obstacle>(new(100, 100));
    }

    public void Run(bool isSimulation)
    {
        Initialize();

        if (!isSimulation)
        {
            Gfx.Provider.StartRenderingLoop(UpdateFrame);
        }
        else
        {
            var frameTime = 0.1f;

            var simulationDuration = 60f;

            var steps = simulationDuration / frameTime;

            for (var i = 0; i < steps; i++)
            {
                Update(frameTime);
            }

        }
    }

    public void UpdateFrame(float delta)
    {
        Update(delta);
        Gfx.Provider.CameraPosition = Player.Position;
        Gfx.Provider.Draw(_state.IsDebug, DrawUI);
    }

    private void Update(float delta)
    {
        Input.GetInput();

        if (Input.Current.DebugTogglePressed)
        {
            _state.IsDebug = !_state.IsDebug;
        }

        if (_state.Scene == Scene.MainMenu)
        {
            if (Input.Current.PausePressed)
            {
                _state.Scene = Scene.Gameplay;
            }
        }
        else
        {
            if (Input.Current.PausePressed)
            {
                _state.IsPaused = !_state.IsPaused;
                _state.Scene = _state.IsPaused ? Scene.PauseMenu : Scene.Gameplay;
            }
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

        if (_state.Scene == Scene.Gameplay)
        {
            var effectiveDelta = delta * _state.TimeScale;
            EntityManager.Update(effectiveDelta);
        }
    }

    private void DrawUI()
    {
        switch (_state.Scene)
        {
            case Scene.Gameplay:
                _hud?.Draw();
                break;
            case Scene.MainMenu:
                _mainMenu.Draw();
                break;
            case Scene.PauseMenu:
                _hud?.Draw();
                _pauseMenu.Draw();
                break;
        }
    }
}

public class GameState()
{
    public Scene Scene { get; internal set; } = Scene.Gameplay;
    public bool IsDebug { get; internal set; } = false;
    public bool IsPaused { get; internal set; } = false;
    public float TimeScale { get; private set; } = 1;

    internal void AlterTimeScale(bool positive)
    {
        TimeScale += 0.1f * (positive ? 1 : -1);
    }
}

public enum Scene
{
    MainMenu,
    Gameplay,
    PauseMenu
}
