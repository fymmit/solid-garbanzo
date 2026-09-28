using Core;
using Events;

internal class PlayerDamageHandler : Behaviour, IDamageable
{
    public int MaxHealth { get; private set; } = 100;
    public int Health { get; private set; } = 100;

    private float _invulnTime = 0;
    private bool _canTakeDamage = true;

    public void TakeDamage(int damage)
    {
        if (_canTakeDamage)
        {
            DamageEventChannel.InvokeDamageEvent(Parent, damage);
            Health -= damage;
            _invulnTime = 0.5f;
            _canTakeDamage = false;
            if (Health <= 0)
            {
                DeathEventChannel.InvokeDeathEvent(Parent);
                Parent.Destroy();
            }
        }
    }

    public override void Update(float delta)
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

