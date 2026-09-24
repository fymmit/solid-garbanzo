using Core;
using Infrastructure;

internal static class GameInput
{
    internal static IInputProvider Provider = new InputProvider();
    internal static Input CurrentInput;

    internal static void GetInput()
    {
        CurrentInput = Provider.GetInput();
    }
}
