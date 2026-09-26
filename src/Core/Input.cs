using Core;
using Core.Providers;

public static class Input
{
    internal static IInputProvider Provider = new RaylibInputProvider();
    public static InputState Current;

    public static void GetInput()
    {
        Current = Provider.GetInput();
    }
}
