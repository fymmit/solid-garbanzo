namespace Core;

public abstract class Behaviour
{
    public Entity Parent { get; private set; } = null!;

    internal void Initialize(Entity parent)
    {
        Parent = parent;
    }

    public virtual void Ready() { }
    public virtual void Update(float delta) { }
}

