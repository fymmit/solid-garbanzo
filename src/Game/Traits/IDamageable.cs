public interface IDamageable
{
    int Health { get; }
    int MaxHealth { get; }
    float HealthPercentage => (float)Health / MaxHealth;
    void TakeDamage(int amount);
}
