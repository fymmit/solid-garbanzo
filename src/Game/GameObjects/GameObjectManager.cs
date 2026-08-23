using System.Numerics;

namespace Game.GameObjects;

internal class GameObjectManager
{
    internal List<GameObject> GameObjects = [];

    internal T Create<T>() where T : GameObject, new()
    {
        var instance = new T();
        GameObjects.Add(instance);

        instance.Ready();

        return instance;
    }

    internal T Create<T>(Vector2 position) where T : GameObject, new()
    {
        var instance = Create<T>();
        instance.Position = position;

        return instance;
    }
}
