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
    private float _timeSinceLastSpawn = 0f;

    private bool _active = true;

    private Entity? _player;
    private int _minRange = 0;
    private int _maxRange = 450;

    public override void Ready()
    {
        _player = _entityManager.Find<Player>().FirstOrDefault();
        DeathEventChannel.DeathEvent += OnDeath;
    }

    public override void Update(float delta)
    {
        _timeSinceLastSpawn -= delta;
        if (_active && _timeSinceLastSpawn <= 0)
        {
            SpawnEnemy();
            _timeSinceLastSpawn = SPAWN_TIMER;
        }
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
        _entityManager.Create<Enemy>(new Vector2(x, y));
    }

    void OnDeath(Entity? entity)
    {
        if (entity is Player)
        {
            _active = false;
            DeathEventChannel.DeathEvent -= OnDeath;
        }
    }

    public override void OnDestroy()
    {
        DeathEventChannel.DeathEvent -= OnDeath;
    }
}
