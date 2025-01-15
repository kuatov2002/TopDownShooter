using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField]public Item Item { get; private set; }
    public int Quantity { get; private set; }

    // ����������� ��� �������� ������ �����
    public InventorySlot(Item item, int quantity = 1)
    {
        Item = item;
        Quantity = quantity;
    }

    // ����� ��� ������������� ��������
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

    // ����� ��� �������� ��������, ����� ��� ���������� ����� 0
    private void DeleteItem()
    {
        // ������ ��� �������� ��������
    }

    // ����� ��� ���������� ��������� � ����
    public void AddItem(Item newItem, int amount = 1)
    {
        Item = newItem;
        Quantity += amount;
    }
}