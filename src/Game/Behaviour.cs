using Game.Events;
using Game.GameObjects;

namespace Game;

public abstract class Behaviour
{
    public GameObject? Parent { get; private set; }

    internal void Initialize(GameObject parent)
    {
        Parent = parent;
        UpdateEventChannel.UpdateEvent += Update;
        Ready();
    }

    public virtual void Ready() { }
    public virtual void Update(float delta) { }

    public void Destroy()
    {
        UpdateEventChannel.UpdateEvent -= Update;
    }
}

