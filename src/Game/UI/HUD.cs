using Raylib_cs;
using Game.Traits;

namespace Game.UI;

public class HUD() : IRenderable, IUpdatable
{
    public void Render()
    {
        var position = Raylib.GetScreenToWorld2D(new System.Numerics.Vector2(10, 10), Program.Camera);
        Raylib.DrawRectangle((int)position.X, (int)position.Y, 160, 40, Color.Black);
        Raylib.DrawText("HUD", (int)position.X + 4, (int)position.Y + 4, 16, Color.White);
    }

    public void Update(float delta) { }
}
