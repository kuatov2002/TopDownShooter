using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;


    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    public List<InventorySlot> Slots=> slots;

    public Item testitem;
    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AddItem(testitem,2);
        AddItem(testitem);
        AddItem(testitem);
        AddItem(testitem);
        AddItem(testitem);
        AddItem(testitem);
    }



    void Start()
    {
    }


    // ����� ��� ���������� �������� � ����
    public void AddItem(Item newItem, int quantity = 1)
    {
        // ���� ���� � ����� �� ���������, ������� ����� �������
        InventorySlot existingSlot = slots.Find(slot => slot.Item.ItemId == newItem.ItemId && slot.Item.IsStackable);

        if (existingSlot != null)
        {
            // ���� �����, ����������� ����������
            existingSlot.AddItem(newItem, quantity);
        }
        else
        {
            // ���� �� �����, ������� ����� ����
            InventorySlot newSlot = new InventorySlot(newItem, quantity);
            slots.Add(newSlot);
        }
    }


    // ����� ��� ������������� �������� �� �����
    public void UseSlot(InventorySlot slot)
    {
        if (slot.Item != null)
        {
            slot.UseItem(); // �������� ����� UseItem() � �����, ������� ���������� �������
        }

        // ���� ���������� ��������� � ����� ����� 0, ������� ���� �� ������
        if (slot.Quantity == 0)
        {
            slots.Remove(slot);
        }
    }
}
