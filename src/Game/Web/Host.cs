using System.Runtime.InteropServices.JavaScript;

namespace Game.Web;

public partial class Host
{
    public static void Main()
    {
        Program.Initialize();
    }

    [JSExport]
    public static void UpdateFrame()
    {
        Program.UpdateFrame();
    }
}
