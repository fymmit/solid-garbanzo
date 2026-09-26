using Events;
using Core;

namespace UI;

public class HUD
{
    public int HealthBarPercentage { get; private set; } = 100;
    public int KillCount { get; private set; } = 0;

    private Player _player;

    public HUD(Player player)
    {
        _player = player;
        DamageEventChannel.DamageEvent += OnDamage;
        DeathEventChannel.DeathEvent += OnDeath;
    }

    public void Draw(GameState state)
    {
        // health bar
        Gfx.Renderer.DrawRectangle(new(8, 8), new(104, 44), 0, 0, 0, 255, false);
        Gfx.Renderer.DrawRectangle(new(10, 10), new(HealthBarPercentage, 40), 255, 0, 0, 255, false);

        // kill counter
        Gfx.Renderer.DrawRectangle(new(396, 10), new(120, 24), 0, 0, 0, 255, false);
        Gfx.Renderer.DrawText($"Kills: {KillCount}", new(400, 14), 16, 255, 255, 255, 255);

        // fps
        Gfx.Renderer.DrawRectangle(new(596, 10), new(120, 24), 0, 0, 0, 255, false);
        Gfx.Renderer.DrawText($"FPS: {Gfx.Renderer.GetFps()}", new(600, 14), 16, 255, 255, 255, 255);

        // timescale
        Gfx.Renderer.DrawRectangle(new(596, 40), new(120, 24), 0, 0, 0, 255, false);
        Gfx.Renderer.DrawText($"Timescale: {state.TimeScale.ToString("N2")}", new(600, 44), 16, 255, 255, 255, 255);

        if (state.IsDebug)
        {
            Gfx.Renderer.DrawRectangle(new(596, 70), new(120, 24), 0, 0, 0, 255, false);
            Gfx.Renderer.DrawText("Debug", new(600, 74), 16, 255, 255, 255, 255);

        }

        var skillbar = _player.GetComponent<Skillbar>();
        if (skillbar is not null)
        {
            for (var i = 0; i < skillbar.Skills.Count; i++)
            {
                var skill = skillbar.Skills[i];
                var cd = skill.CooldownTimer;
                var barWidth = 120;
                var barHeight = 30;
                var paddingOffset = 8;
                var remainingCdBarWidth = cd is null
                    ? barWidth
                    : barWidth - (barWidth * (cd.Duration / cd.OriginalDuration));
                var x = i * 140 + 10;

                Gfx.Renderer.DrawRectangle(new(x, 400), new(barWidth, barHeight + 40), 0, 0, 0, 255, false);
                Gfx.Renderer.DrawText(skill.Name, new(x + 4, 404), 16, 255, 255, 255, 255);
                Gfx.Renderer.DrawRectangle(new(x + 4, 444), new(remainingCdBarWidth - paddingOffset, barHeight - paddingOffset), 255, 255, 255, 255, false);
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
