using System.Numerics;
using Game.Events;

namespace Game.GameObjects;

internal class GameObjectManager
{
    internal List<GameObject> GameObjects = [];
    private List<GameObject> _toBeAdded = [];
    private List<GameObject> _toBeRemoved = [];

    internal GameObjectManager()
    {
        UpdateEventChannel.UpdateLoopStartedEvent += OnUpdateLoopStarted;
    }

    internal T Create<T>() where T : GameObject, new()
    {
        var instance = new T();
        _toBeAdded.Add(instance);

        return instance;
    }

    internal T Create<T>(Vector2 position) where T : GameObject, new()
    {
        var instance = Create<T>();
        instance.Position = position;

        return instance;
    }

    internal void Remove(GameObject gameObject)
    {
        _toBeRemoved.Add(gameObject);
    }

    void OnUpdateLoopStarted()
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
