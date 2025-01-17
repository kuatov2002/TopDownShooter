using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryManager : MonoBehaviour, IInventoryManager
{

    public event Action OnInventoryChanged;

    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();
    public List<InventorySlot> Slots => slots;

    private InventoryWindow _inventoryWindow;

    [Inject]
    public void Construct(InventoryWindow inventoryWindow)
    {
        _inventoryWindow = inventoryWindow;
    }

    public void AddItem(Item newItem, int quantity = 1)
    {
        InventorySlot existingSlot = slots.Find(slot => slot.Item.ItemId == newItem.ItemId && slot.Item.IsStackable);

        if (existingSlot != null)
        {
            existingSlot.AddItem(newItem, quantity);
        }
        else
        {
            InventorySlot newSlot = new InventorySlot(newItem, quantity);
            slots.Add(newSlot);
        }

        NotifyInventoryChanged();
    }
    private void NotifyInventoryChanged()
    {
        OnInventoryChanged?.Invoke();
    }

    public void UseSlot(InventorySlot slot)
    {
        if (slot.Item != null)
        {
            slot.UseItem();
        }

        if (slot.Quantity == 0)
        {
            slots.Remove(slot);
        }

        NotifyInventoryChanged();
    }
    public void RemoveSlot(InventorySlot slot)
    {
        if (slots.Contains(slot))
        {
            slots.Remove(slot); // Удаляем слот из списка
            NotifyInventoryChanged(); // Обновляем UI
        }
    }
}
