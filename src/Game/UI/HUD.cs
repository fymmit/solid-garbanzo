using Events;
using Core;

namespace UI;

public class HUD
{
    public int HealthBarPercentage { get; private set; } = 100;

    public HUD()
    {
        DamageEventChannel.DamageEvent += OnDamage;
    }

    private void OnDamage(Entity? target, int amount)
    {
        if (target is Player)
        {
            HealthBarPercentage -= amount;
        }
    }
}
