using Events;
using Core;

namespace UIElements;

public class HUD
{
    public int HealthBarPercentage { get; private set; } = 100;
    public int KillCount { get; private set; } = 0;

    private Player _player;
    private GameState _state;

    public HUD(Player player, GameState state)
    {
        _player = player;
        _state = state;
        DamageEventChannel.DamageEvent += OnDamage;
        DeathEventChannel.DeathEvent += OnDeath;
    }

    public void Draw()
    {
        // health bar
        UI.ProgressBar.Draw
        (
            new(10, 10),
            new(100, 40),
            HealthBarPercentage / 100f,
            (255, 0, 0, 255),
            (0, 0, 0, 255),
            2
        );

        UI.Button.Draw
        (
            new(10, 80),
            new(80, 40),
            (150, 150, 150, 200),
            (200, 200, 200, 150),
            () => Console.WriteLine("Button pressed"),
            null
        );

        // kill counter
        Gfx.Provider.DrawRectangle(new(396, 10), new(120, 24), 0, 0, 0, 255, false);
        Gfx.Provider.DrawText($"Kills: {KillCount}", new(400, 14), 16, 255, 255, 255, 255);

        // fps
        Gfx.Provider.DrawRectangle(new(596, 10), new(120, 24), 0, 0, 0, 255, false);
        Gfx.Provider.DrawText($"FPS: {Gfx.Provider.GetFps()}", new(600, 14), 16, 255, 255, 255, 255);

        // timescale
        Gfx.Provider.DrawRectangle(new(596, 40), new(120, 24), 0, 0, 0, 255, false);
        Gfx.Provider.DrawText($"Timescale: {_state.TimeScale.ToString("N2")}", new(600, 44), 16, 255, 255, 255, 255);

        if (_state.IsDebug)
        {
            Gfx.Provider.DrawRectangle(new(596, 70), new(120, 24), 0, 0, 0, 255, false);
            Gfx.Provider.DrawText("Debug", new(600, 74), 16, 255, 255, 255, 255);

        }

        var skillbar = _player.GetComponent<Skillbar>();
        if (skillbar is not null)
        {
            for (var i = 0; i < skillbar.Skills.Count; i++)
            {
                var skill = skillbar.Skills[i];
                var cd = skill.CooldownTimer;
                var barWidth = 120;
                var barHeight = 24;
                var x = i * 140 + 10;

                Gfx.Provider.DrawRectangle(new(x, 400), new(barWidth, barHeight), 0, 0, 0, 255, false);
                Gfx.Provider.DrawText(skill.Name, new(x + 4, 404), 16, 255, 255, 255, 255);

                UI.ProgressBar.Draw
                (
                    new(x, 428),
                    new(barWidth, barHeight),
                    cd is null ? 1f : 1f - (cd.Duration / cd.OriginalDuration),
                    (255, 255, 255, 255),
                    (0, 0, 0, 255),
                    4
                );
            }
        }
    }

    private void OnDamage(Entity? target, int amount)
    {
        if (target is Player)
        {
            HealthBarPercentage -= amount;
        }
    }

    private void OnDeath(Entity? target)
    {
        if (target is Enemy)
        {
            KillCount++;
        }
    }
}
