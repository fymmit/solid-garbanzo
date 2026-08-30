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
