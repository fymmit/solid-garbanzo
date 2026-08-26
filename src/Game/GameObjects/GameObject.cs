using System.Numerics;
using Game.Traits;

namespace Game.GameObjects;

public class GameObject : IUpdatable, IDebugRenderable
{
    public IComposable[] Components { get; protected set; }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }

    internal GameObject()
    {
        Components = [];
        Position = new();
    }

    public virtual void Ready() { }

    public virtual void Update(float delta) { }

    public virtual void DebugRender() { }

    internal IEnumerable<T> GetComponents<T>()
    {
        return Components.OfType<T>();
    }

    internal T? GetComponent<T>()
    {
        if (this is T component)
        {
            return component;
        }
        return GetComponents<T>().FirstOrDefault();
    }

    public void Destroy()
    {
        GameObjectManager.Remove(this);
    }
}


