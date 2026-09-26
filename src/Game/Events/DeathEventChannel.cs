using Core;

namespace Events;

public static class DeathEventChannel
{
    public static event Action<Entity?>? DeathEvent;

    public static void InvokeDeathEvent(Entity? target)
    {
        Logger.Log($"{target} died");
        DeathEvent?.Invoke(target);
    }
}

