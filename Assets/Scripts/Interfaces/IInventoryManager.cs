using System;
using System.Collections.Generic;

public interface IInventoryManager
{
    public event Action OnInventoryChanged;
    void AddItem(Item newItem, int quantity = 1);
    void UseSlot(InventorySlot slot);
    void RemoveSlot(InventorySlot slot);
    List<InventorySlot> Slots { get; }
}
