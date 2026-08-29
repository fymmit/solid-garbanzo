using System.Numerics;
using Core.Events;

namespace Core;

public static class EntityManager
{
    public static List<Entity> Entities { get; private set; } = [];
    private static List<Entity> _toBeAdded = [];
    private static List<Entity> _toBeRemoved = [];

    public static void Setup()
    {
        UpdateEventChannel.UpdateLoopStartedEvent += OnUpdateLoopStarted;
    }

    public static T Create<T>() where T : Entity, new()
    {
        var instance = new T();
        _toBeAdded.Add(instance);

        return instance;
    }

    public static T Create<T>(Vector2 position) where T : Entity, new()
    {
        var instance = Create<T>();
        instance.Position = position;

        return instance;
    }

    internal static void Remove(Entity gameObject)
    {
        _toBeRemoved.Add(gameObject);
    }

    public static IEnumerable<T> Find<T>() where T : Entity
    {
        return Entities.OfType<T>();
    }

    private static void OnUpdateLoopStarted()
    {
        var pendingRemovals = _toBeRemoved;
        _toBeRemoved = [];
        foreach (var entity in pendingRemovals)
        {
            Entities.Remove(entity);
        }

        var pendingAdditions = _toBeAdded;
        _toBeAdded = [];
        foreach (var entity in pendingAdditions)
        {
            Entities.Add(entity);
        }
        foreach (var entity in pendingAdditions)
        {
            entity.Activate();
        }
    }
}
