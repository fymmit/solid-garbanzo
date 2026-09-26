using System.Numerics;
using Core;
using Infrastructure;

internal static class Input
{
    internal static IInputProvider Provider = new InputProvider();
    internal static InputState Current;

    internal static void GetInput(Vector2 cameraPosition)
    {
        Current = Provider.GetInput(cameraPosition);
    }
}
