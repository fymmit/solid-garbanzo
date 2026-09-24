using System.Numerics;
using Core;
using Events;

public class EnemySpawner : Entity
{
    public EnemySpawner()
    {
        Attach<EnemySpawnerBehaviour>();
    }
}

public class EnemySpawnerBehaviour : Behaviour
{
    private const float SPAWN_TIMER = 1f;
    private TimerInstance? _timer;

    private Entity? _player;
    private int _minRange = 0;
    private int _maxRange = 450;

    public override void Ready()
    {
        _player = EntityManager.Find<Player>().FirstOrDefault();
        DeathEventChannel.DeathEvent += OnDeath;

        _timer = Timers.CreateInterval(SPAWN_TIMER, () => SpawnEnemy());
    }

    void SpawnEnemy()
    {
        var random = new Random();
        float x = random.Next(_minRange, _maxRange);
        float y = random.Next(_minRange, _maxRange);

        if (x > y)
        {
            x = _maxRange;
        }
        else
        {
            y = _maxRange;
        }

        if (random.Next(0, 2) is 0)
        {
            x = -x;
        }
        if (random.Next(0, 2) is 0)
        {
            y = -y;
        }
        if (_player is not null)
        {
            x += _player.Position.X;
            y += _player.Position.Y;
        }
        EntityManager.Create<Enemy>(new Vector2(x, y));
    }

    void OnDeath(Entity? entity)
    {
        if (entity is Player)
        {
            Parent.Destroy();
        }
    }

    public override void OnDestroy()
    {
        DeathEventChannel.DeathEvent -= OnDeath;
        _timer.Remove();
    }
}
