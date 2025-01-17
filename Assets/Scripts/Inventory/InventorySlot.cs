public class InventorySlot
{
    public Item Item { get; private set; } // Предмет в слоте
    public int Quantity { get; private set; } // Количество предметов в слоте

    // Конструктор для создания нового слота
    public InventorySlot(Item item, int quantity = 1)
    {
        Item = item;
        Quantity = quantity;
    }

    public void AddItem(int amount)
    {
        if (!Item.IsStackable)
        {
        }

        Quantity += amount;
    }

    public int UseItem()
    {
        if (Quantity <= 0)
        {
            return 0; // Нет предметов для использования
        }

        Item.Use(); // Вызов действия, связанного с использованием предмета
        Quantity--;  // Уменьшаем количество предметов

        return Quantity; // Возвращаем оставшееся количество
    }


    public void SetQuantity(int quantity)
    {
        if (quantity < 0)
        {
        }

        Quantity = quantity;
    }

    public bool CanAddItem(Item item)
    {
        return Item.ItemId == item.ItemId && Item.IsStackable;
    }
}