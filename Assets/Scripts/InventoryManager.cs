using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;


    private List<InventorySlot> slots = new List<InventorySlot>();

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

        AddItem(testitem);
        AddItem(testitem);
        
    }



    void Start()
    {

    }


    // ����� ��� ���������� �������� � ����
    public void AddItem(Item newItem, int quantity = 1)
    {
        print($"added {newItem} quant {quantity}");
        // ���������, ���� �� ��� ���� � ����� ���������
        InventorySlot existingSlot = slots.Find(slot => slot.Item.ItemId == newItem.ItemId && slot.Item.IsStackable);

        if (existingSlot != null)
        {
            // ���� ���� ������, ��������� ����������
            existingSlot.AddItem(newItem, quantity);
        }
        else
        {
            InventorySlot newSlot = new InventorySlot(newItem, quantity);
            slots.Add(newSlot);
        }

        // ��������� UI ����� ���������� ��������
        //UpdateUI();
    }

    // ����� ��� ������������� �������� �� �����
    void UseItem(InventorySlot slot)
    {
        if (slot.Item != null)
        {
            slot.UseItem(); // �������� ����� UseItem() � �����, ������� ���������� �������
        }
        //UpdateUI();
    }
}
