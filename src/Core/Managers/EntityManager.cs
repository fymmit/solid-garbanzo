using System.Numerics;

namespace Core;

public static class EntityManager
{
    public static List<Entity> Entities { get; private set; } = [];
    private static List<Entity> _toBeAdded = [];

    public static T Create<T>() where T : Entity, new()
    {
        var instance = new T();
        Console.WriteLine($"{instance} created");
        _toBeAdded.Add(instance);

        return instance;
    }

    public static T Create<T>(Vector2 position) where T : Entity, new()
    {
        var instance = Create<T>();
        instance.Position = position;

        return instance;
    }

    public static IEnumerable<T> Find<T>() where T : Entity
    {
        return Entities.OfType<T>();
    }

    public static void ProcessPendingEntities()
    {
        Entities.RemoveAll(e => e.IsMarkedForDestruction);

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
