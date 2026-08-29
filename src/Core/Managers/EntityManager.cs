using System.Numerics;

namespace Core;

public class EntityManager
{
    public List<Entity> Entities { get; private set; } = [];
    private List<Entity> _toBeAdded = [];

    public T Create<T>() where T : Entity, new()
    {
        var instance = new T();
        _toBeAdded.Add(instance);

        return instance;
    }

    public T Create<T>(Vector2 position) where T : Entity, new()
    {
        var instance = Create<T>();
        instance.Position = position;

        return instance;
    }

    public IEnumerable<T> Find<T>() where T : Entity
    {
        return Entities.OfType<T>();
    }

    public void ProcessPendingEntities()
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
            entity.Activate(this);
        }
    }
}
