using Events;
using Core;

namespace UI;

public class HUD
{
    public int HealthBarPercentage { get; private set; } = 100;
    public int KillCount { get; private set; } = 0;

    public HUD()
    {
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

        // TODO: rethink cooldown drawing with the new Timers implementation

        // var skillbar = Game.Player.GetComponent<Skillbar>();
        // if (skillbar is not null)
        // {
        //     for (var i = 0; i < skillbar.Skills.Count; i++)
        //     {
        //         var skill = skillbar.Skills[i];
        //         var cd = skill.GetComponent<Cooldown>();
        //         if (cd is not null)
        //         {
        //             var text = cd.IsReady() ? "Ready" : cd.RemainingCooldown.ToString("N1");
        //             var x = i * 140 + 10;
        //             DrawRectangle(x, GetScreenHeight() - 50, 120, 30, Color.Black);
        //             DrawText(text, x + 4, GetScreenHeight() - 46, 24, Color.White);
        //         }
        //     }
        // }
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
