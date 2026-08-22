namespace Game.GameObjects;

internal class GameObject
{
    internal GameObject()
    {
        Ready();
    }

    internal virtual void Ready()
    {
        Console.WriteLine("GameObject ready");
    }

    internal virtual void Update() { }
}
