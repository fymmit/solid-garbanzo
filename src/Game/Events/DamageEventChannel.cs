using Core;

namespace Events;

public static class DamageEventChannel
{
    public static event Action<Entity?, int>? DamageEvent;

    public static void InvokeDamageEvent(Entity? target, int damage)
    {
        DamageEvent?.Invoke(target, damage);
        Console.WriteLine($"Damage event invoked for {target} with {damage} damage");
    }
}

