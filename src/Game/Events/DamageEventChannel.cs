using Game.GameObjects;

namespace Game.Events;

public static class DamageEventChannel
{
    public static event Action<GameObject?, int>? DamageEvent;

    public static void InvokeDamageEvent(GameObject? target, int damage)
    {
        DamageEvent?.Invoke(target, damage);
        Console.WriteLine($"Damage event invoked for {target} with {damage} damage");
    }
}

