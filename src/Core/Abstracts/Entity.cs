using System.Numerics;

namespace Core;

public abstract class Entity
{
    public List<Behaviour> Components { get; } = [];

    public Vector2 Position { get; set; } = new();
    public float Rotation { get; set; }

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
        EntityManager.Remove(this);
    }

    protected void Attach<T>() where T : Behaviour, new()
    {
        var behaviour = new T();
        behaviour.Initialize(this);
        Components.Add(behaviour);
        if (_activated)
        {
            behaviour.Ready();
        }
    }

    internal void Activate()
    {
        if (!_activated)
        {
            _activated = true;
            var initialComponents = Components.ToArray();
            foreach (var component in initialComponents)
            {
                component.Ready();
            }
        }
    }
}
