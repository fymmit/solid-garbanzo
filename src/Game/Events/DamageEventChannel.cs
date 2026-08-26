using Game.Traits;

namespace Game.Events;

public static class DamageEventChannel
{
    public static event Action<IDamageable, int>? DamageEvent;

    public static void InvokeDamageEvent(IDamageable target, int damage)
    {
        DamageEvent?.Invoke(target, damage);
        Console.WriteLine($"Damage event invoked for {target} with {damage} damage");
    }
}

