using System.Numerics;
using Game.Events;

namespace Game.GameObjects;

internal static class GameObjectManager
{
    internal static List<GameObject> GameObjects { get; private set; } = [];
    private static List<GameObject> _toBeAdded = [];
    private static List<GameObject> _toBeRemoved = [];

    internal static void Setup()
    {
        UpdateEventChannel.UpdateLoopStartedEvent += OnUpdateLoopStarted;
    }

    internal static T Create<T>() where T : GameObject, new()
    {
        var instance = new T();
        _toBeAdded.Add(instance);

        return instance;
    }

    internal static T Create<T>(Vector2 position) where T : GameObject, new()
    {
        var instance = Create<T>();
        instance.Position = position;

        return instance;
    }

    internal static void Remove(GameObject gameObject)
    {
        _toBeRemoved.Add(gameObject);
    }

    private static void OnUpdateLoopStarted()
    {
        foreach (var tbr in _toBeRemoved)
        {
            GameObjects.Remove(tbr);
        }
        foreach (var tba in _toBeAdded)
        {
            GameObjects.Add(tba);
            tba.Ready();
        }
        _toBeRemoved = [];
        _toBeAdded = [];
    }
}
