using System.Numerics;

namespace Core;

public abstract class Entity
{
    public bool IsMarkedForDestruction { get; private set; }
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
        if (IsMarkedForDestruction)
        {
            return;
        }

        IsMarkedForDestruction = true;

        foreach (var component in Components)
        {
            component.OnDestroy();
        }
    }

    protected void Attach(Behaviour behaviour)
    {
        Components.Add(behaviour);
        if (_activated)
        {
            behaviour.Initialize(this);
            behaviour.Ready();
        }
    }

    protected void Attach<T>() where T : Behaviour, new()
    {
        Attach(new T());
    }

    internal void Activate()
    {
        if (!_activated)
        {
            _activated = true;
            var initialComponents = Components.ToArray();
            foreach (var component in initialComponents)
            {
                component.Initialize(this);
                component.Ready();
            }
        }
    }
}
