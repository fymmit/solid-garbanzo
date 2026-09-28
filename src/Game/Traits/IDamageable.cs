public interface IDamageable
{
    int Health { get; }
    int MaxHealth { get; }
    void TakeDamage(int amount);
}
