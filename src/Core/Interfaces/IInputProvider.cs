using System.Numerics;

namespace Core;

public interface IInputProvider
{
    InputState GetInput(Vector2 cameraPosition);
}
