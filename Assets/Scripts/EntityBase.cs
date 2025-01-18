using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class LootItem
{
    public Item item;        // Предмет для выпадения
    [Range(0f, 1f)]         // Ограничиваем значение от 0 до 1 (0-100%)
    public float dropChance; // Шанс выпадения
}
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

    [SerializeField]
    protected LootItem[] possibleLoot;
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
            GenerateLoot();
        }
    }

    public virtual void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke();
    }
    private void GenerateLoot()
    {
        if (possibleLoot == null) return;

        foreach (var loot in possibleLoot)
        {
            if (Random.value <= loot.dropChance)
            {
                GameObject currentLoot = new GameObject(loot.item.DisplayName);

                Vector3 randomOffset = new Vector3(
                    Random.Range(-0.4f, 0.4f),
                    Random.Range(-0.4f, 0.4f),
                    0

                );
                currentLoot.transform.position = transform.position + randomOffset;
                currentLoot.layer = LayerMask.NameToLayer("Loot");
                SpriteRenderer spriteRenderer=currentLoot.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = loot.item.Icon;
                Loot lootComponent = currentLoot.AddComponent<Loot>();
                lootComponent.Item = loot.item;

                CircleCollider2D collider = currentLoot.AddComponent<CircleCollider2D>();
                collider.isTrigger = true;
            }
        }
    }
}