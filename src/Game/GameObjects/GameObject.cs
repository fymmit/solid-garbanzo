using System.Numerics;
using Game.GameObjects.Traits;

namespace Game.GameObjects;

public class GameObject : IUpdatable
{
    public IComposable[] Components { get; protected set; }
    public Vector2 Position { get; set; }

    internal GameObject()
    {
        Components = [];
        Position = new();
    }

    internal virtual void Ready() { }

    public virtual void Update(float delta) { }

    internal T? GetComponent<T>()
    {
        return Components.OfType<T>().FirstOrDefault();
    }

    internal IEnumerable<T> GetComponents<T>()
    {
        return Components.OfType<T>();
    }
}

