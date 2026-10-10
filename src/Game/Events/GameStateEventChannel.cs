using Core;

namespace Events;

public static class GameStateEventChannel
{
    public static event Action<Scene>? SceneChangedEvent;

    public static void InvokeSceneChangedEvent(Scene scene)
    {
        Logger.Log($"Scene changed to: {scene}");
        SceneChangedEvent?.Invoke(scene);
    }
}

