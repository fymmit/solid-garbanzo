using System.Numerics;

namespace Core;

public interface IInputProvider
{
    Input GetInput();
    Vector2 GetMouseScreenPosition();
    Vector2 GetMouseWorldPosition(Vector2 offset);
}
