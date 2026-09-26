using System.Runtime.InteropServices.JavaScript;

namespace RaylibHost;

public static partial class Program
{
    private static readonly GameRuntime Game = new();

    public static void Main()
    {
        Game.Initialize();
    }

    [JSExport]
    public static void UpdateFrame(float delta)
    {
        Game.UpdateFrame(delta);
    }
}
