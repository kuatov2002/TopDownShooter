using UnityEngine;
using UnityEngine.UI;

public abstract class EntityBase : MonoBehaviour, IHealth
{
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    public event System.Action OnHealthChanged;
    public event System.Action OnDeath;
    [SerializeField] protected Slider hpBar;
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        OnHealthChanged?.Invoke();

        if (IsDead)
        {
            OnDeath?.Invoke();
        }
    }

    public virtual void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke();
    }
}