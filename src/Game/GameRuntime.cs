using Core;
using UI;

public sealed class GameRuntime
{
    private EntityManager _entityManager = new();
    public Player Player { get; private set; } = null!;
    public HUD Hud { get; } = new();

    public IEnumerable<Entity> Entities => _entityManager.Entities;

    public void Initialize()
    {
        Player = _entityManager.Create<Player>();
        _entityManager.Create<EnemySpawner>();
        _entityManager.Create<Obstacle>(new(100, 100));
    }

    public void Update(float delta, GameInput input)
    {
        CurrentInput.Value = input;
        _entityManager.ProcessPendingEntities();

        foreach (var entity in _entityManager.Entities)
        {
            foreach (var component in entity.Components)
            {
                component.Update(delta);
            }
        }
    }
}
