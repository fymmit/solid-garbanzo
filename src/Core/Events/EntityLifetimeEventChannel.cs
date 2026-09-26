namespace Core.Events;

public static class EntityLifetimeEventChannel
{
    public static event Action<Entity>? CreateEvent;
    public static event Action<Entity>? DestroyEvent;

    public static void InvokeCreateEvent(Entity entity)
    {
        Logger.Log($"{entity} created");
        CreateEvent?.Invoke(entity);
    }

    public static void InvokeDestroyEvent(Entity entity)
    {
        Logger.Log($"{entity} destroyed");
        DestroyEvent?.Invoke(entity);
    }
}

