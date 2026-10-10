using Events;

namespace UIElements;

public class MainMenu
{
    public void Draw()
    {
        UI.Button.Draw(
            new(200, 200),
            new(300, 100),
            (200, 200, 200, 255),
            (150, 150, 150, 255),
            () => GameStateEventChannel.InvokeSceneChangedEvent(Scene.Gameplay),
            null);
        Gfx.Provider.DrawText("Start game", new(240, 240), 32, 0, 0, 0, 255);
    }
}
