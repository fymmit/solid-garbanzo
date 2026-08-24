using System.Numerics;
using Game.Traits;

namespace Game.GameObjects;

public class GameObject : IUpdatable, IDebugRenderable
{
    public IComposable[] Components { get; protected set; }
    public Vector2 Position { get; set; }

    internal GameObject()
    {
        Components = [];
        Position = new();
    }

    public virtual void Ready() { }

    public virtual void Update(float delta) { }

    public virtual void DebugRender() { }

    internal T? GetComponent<T>()
    {
        return Components.OfType<T>().FirstOrDefault();
    }

    internal IEnumerable<T> GetComponents<T>()
    {
        return Components.OfType<T>();
    }
}


