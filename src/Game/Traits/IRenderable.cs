using System.Numerics;

namespace Game.Traits;

internal interface IRenderable
{
    Vector2 Position { get; }
    void Render();
}
