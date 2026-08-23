using System.Numerics;

namespace Game.GameObjects;

internal class GameObject
{
    public Vector2 Position { get; set; }

    internal GameObject()
    {
        Position = new();
    }

    internal virtual void Ready() { }

    internal virtual void Update(float delta) { }

    internal virtual void Draw() { }
}
