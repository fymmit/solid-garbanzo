namespace UIElements;

public class PauseMenu
{
    public void Draw()
    {
        Gfx.Provider.DrawRectangle(new(296, 80), new(120, 24), 0, 0, 0, 255, false);
        Gfx.Provider.DrawText("Paused", new(300, 84), 16, 255, 255, 255, 255);
    }
}
