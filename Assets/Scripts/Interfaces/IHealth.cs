public interface IHealth
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    bool IsDead { get; }
    void TakeDamage(float damage);
    void Heal(float amount);
    event System.Action OnHealthChanged;
    event System.Action OnDeath;
}