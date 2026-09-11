namespace Core;

public abstract class Behaviour
{
    protected EntityManager _entityManager = null!;
    public Entity Parent { get; private set; } = null!;

    internal void Initialize(Entity parent, EntityManager entityManager)
    {
        Parent = parent;
        _entityManager = entityManager;
    }

    public virtual void Ready() { }
    public virtual void Update(float delta) { }
    public virtual void OnDestroy() { }
}

