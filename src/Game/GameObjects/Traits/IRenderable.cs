using System.Numerics;

namespace Game.GameObjects.Traits;

internal interface IRenderable
{
    Vector2 Position { get; }
    void Render();
}
