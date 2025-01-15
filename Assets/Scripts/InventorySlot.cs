using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField]public Item Item { get; private set; }
    public int Quantity { get; private set; }

    // Конструктор для создания нового слота
    public InventorySlot(Item item, int quantity = 1)
    {
        Item = item;
        Quantity = quantity;
    }

    // Метод для использования предмета
    public void UseItem()
    {
        if (Item != null)
        {
            Item.Use();
            Quantity--;
            if (Quantity <= 0)
            {
                DeleteItem();
            }
        }
    }

    // Метод для удаления предмета, когда его количество стало 0
    private void DeleteItem()
    {
        // Логика для удаления предмета
    }

    // Метод для добавления предметов в слот
    public void AddItem(Item newItem, int amount = 1)
    {
        Item = newItem;
        Quantity += amount;
    }
}