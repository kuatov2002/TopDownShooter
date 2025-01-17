using UnityEngine;

public class LootCollector : MonoBehaviour, ILootCollector
{
    [SerializeField] private LayerMask lootLayer; // Слой с предметами лута
    [SerializeField] private float collectRadius = 2f; // Радиус сбора

    private IInventoryManager inventoryManager;

    // Инициализация инвентаря через интерфейс
    public void Initialize(IInventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, находится ли объект на слое лута
        if ((1 << collision.gameObject.layer & lootLayer) != 0)
        {
            Loot loot = collision.GetComponent<Loot>(); // Извлекаем компонент Loot
            if (loot != null)
            {
                // Добавляем предмет в инвентарь
                inventoryManager.AddItem(loot.Item);
                Destroy(collision.gameObject); // Удаляем лут
            }
        }
    }

    // Для отладки в редакторе рисуем радиус сбора
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectRadius);
    }
}