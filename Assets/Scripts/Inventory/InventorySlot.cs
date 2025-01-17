public class InventorySlot
{
    private Item item;
    private int quantity;

    public Item Item => item;
    public int Quantity => quantity;
    // ����������� ��� �������� ������ �����
    public InventorySlot(Item item, int quantity = 1)
    {
        this.item = item;
        this.quantity = quantity;
    }

    // ����� ��� ������������� ��������
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            quantity--;
        }
    }


    // ����� ��� ���������� ��������� � ����
    public void AddItem(Item newItem, int amount = 1)
    {
        item = newItem;
        quantity += amount;
    }
}