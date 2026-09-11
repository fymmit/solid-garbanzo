using System.Numerics;

namespace Core;

public abstract class Entity
{
    public bool IsMarkedForDestruction { get; private set; }
    protected EntityManager _entityManager = null!;
    public List<Behaviour> Components { get; } = [];

    public Vector2 Position { get; set; } = new();
    public float Rotation { get; set; }
    public float Radius { get; set; }

    private bool _activated = false;

    public IEnumerable<T> GetComponents<T>()
    {
        return Components.OfType<T>();
    }

    public T? GetComponent<T>()
    {
        return GetComponents<T>().FirstOrDefault();
    }

    public void Destroy()
    {
        foreach (var component in Components)
        {
            component.OnDestroy();
        }
        IsMarkedForDestruction = true;
    }

    protected void Attach(Behaviour behaviour)
    {
        Components.Add(behaviour);
        if (_activated)
        {
            behaviour.Initialize(this, _entityManager);
            behaviour.Ready();
        }
    }

    protected void Attach<T>() where T : Behaviour, new()
    {
        Attach(new T());
    }

    internal void Activate(EntityManager entityManager)
    {
        _entityManager = entityManager;
        if (!_activated)
        {
            _activated = true;
            var initialComponents = Components.ToArray();
            foreach (var component in initialComponents)
            {
                component.Initialize(this, _entityManager);
                component.Ready();
            }
        }
    }
}
