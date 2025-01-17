public class InventorySlot
{
    public Item Item { get; private set; } // ������� � �����
    public int Quantity { get; private set; } // ���������� ��������� � �����

    // ����������� ��� �������� ������ �����
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
            return 0; // ��� ��������� ��� �������������
        }

        Item.Use(); // ����� ��������, ���������� � �������������� ��������
        Quantity--;  // ��������� ���������� ���������

        return Quantity; // ���������� ���������� ����������
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