using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryManager : IInventoryManager
{
    public event Action<List<InventorySlot>> OnInventoryChanged;

    private readonly List<InventorySlot> slots = new();

    public List<InventorySlot> Slots => new List<InventorySlot>(slots); // ���������� ����� ������ ��� ������������

    public void AddItem(Item newItem, int quantity = 1)
    {
        var existingSlot = slots.Find(slot => slot.Item.ItemId == newItem.ItemId && slot.Item.IsStackable);

        if (existingSlot != null)
        {
            existingSlot.AddItem(quantity);
        }
        else
        {
            slots.Add(new InventorySlot(newItem, quantity));
        }

        NotifyInventoryChanged();
    }

    public void UseSlot(InventorySlot slot)
    {
        if (slot.UseItem() == 0)
        {
            slots.Remove(slot);
        }

        NotifyInventoryChanged();
    }

    public void RemoveSlot(InventorySlot slot)
    {
        if (slots.Contains(slot))
        {
            slots.Remove(slot);
            NotifyInventoryChanged();
        }
    }

    public bool CanAddItem(Item newItem)
    {
        var existingSlot = slots.Find(slot => slot.Item.ItemId == newItem.ItemId && slot.Item.IsStackable);

        if (existingSlot != null)
        {
            return true; // ���� ����� � ������������ �����
        }

        return slots.Count < MaxSlotCount; // MaxSlotCount � �������� ����������� ������
    }

    public int GetItemCount(Item item)
    {
        int count = 0;

        foreach (var slot in slots)
        {
            if (slot.Item.ItemId == item.ItemId)
            {
                count += slot.Quantity;
            }
        }

        return count;
    }

    private void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke(new List<InventorySlot>(slots)); // ������� ����� ������ ��� ������������
    }

    // ����������� ���������� ������ (����� ���������)
    private const int MaxSlotCount = 20;
}
