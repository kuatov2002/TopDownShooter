public class InventorySlot
{
    private Item item;
    private int quantity;

    public Item Item => item;
    public int Quantity => quantity;
    // Конструктор для создания нового слота
    public InventorySlot(Item item, int quantity = 1)
    {
        this.item = item;
        this.quantity = quantity;
    }

    // Метод для использования предмета
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            quantity--;
        }
    }


    // Метод для добавления предметов в слот
    public void AddItem(Item newItem, int amount = 1)
    {
        item = newItem;
        quantity += amount;
    }
}