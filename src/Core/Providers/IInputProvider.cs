using System.Numerics;

namespace Core.Providers;

public interface IInputProvider
{
    InputState GetInput(Vector2 cameraPosition);
}
