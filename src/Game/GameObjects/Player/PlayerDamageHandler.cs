using Game.Traits;
using Game.Events;

namespace Game.GameObjects.Player;

internal class PlayerDamageHandler(GameObject parent) : IComposable, IDamageable, IUpdatable
{
    public GameObject Parent => parent;
    public int Health { get; private set; } = 100;

    private float _invulnTime = 0;
    private bool _canTakeDamage = true;

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage)
        {
            DamageEventChannel.InvokeDamageEvent(this, damage);
            Health -= damage;
            _invulnTime = 0.5f;
            _canTakeDamage = false;
            if (Health <= 0)
            {
                Parent.Destroy();
            }
        }
    }

    public void Update(float delta)
    {
        if (!_canTakeDamage && _invulnTime > 0)
        {
            _invulnTime -= delta;
            if (_invulnTime <= 0)
            {
                _canTakeDamage = true;
            }
        }
    }
}


