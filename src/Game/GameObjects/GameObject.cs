using System.Numerics;
using Game.Traits;

namespace Game.GameObjects;

public class GameObject : IDebugRenderable
{
    public List<Behaviour> Components { get; } = [];
    public Renderer? Renderer { get; protected set; }

    public Vector2 Position { get; set; }
    public float Rotation { get; set; }

    internal GameObject()
    {
        Position = new();
    }

    public virtual void DebugRender() { }

    internal IEnumerable<T> GetComponents<T>()
    {
        return Components.OfType<T>();
    }

    internal T? GetComponent<T>()
    {
        return GetComponents<T>().FirstOrDefault();
    }

    public void Destroy()
    {
        foreach (var behaviour in Components)
        {
            behaviour.Destroy();
        }
        GameObjectManager.Remove(this);
    }

    protected void Attach<T>() where T : Behaviour, new()
    {
        var behaviour = new T();
        behaviour.Initialize(this);
        Components.Add(behaviour);
    }
}

