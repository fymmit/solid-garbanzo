using System.Numerics;

namespace Game.GameObjects;

internal class GameObject
{
    internal static T Create<T>() where T : GameObject, new() => new T();
    internal static T Create<T>(Vector2 position) where T : GameObject, new()
    {
        var instance = new T();
        instance.Position = position;
        return instance;
    }

    public Vector2 Position { get; protected set; }

    internal GameObject()
    {
        Position = new();
    }

    internal virtual void Update(float delta) { }
}
