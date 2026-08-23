namespace Game.Events;

public static class DamageEventChannel
{
    public static event Action<int>? DamageEvent;

    public static void InvokeDamageEvent(int damage)
    {
        DamageEvent?.Invoke(damage);
        Console.WriteLine($"Damage event invoked with {damage} damage");
    }
}
