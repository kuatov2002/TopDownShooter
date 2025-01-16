using System;
using System.Collections.Generic;

public interface IInventoryManager
{
    void AddItem(Item newItem, int quantity = 1);
    void UseSlot(InventorySlot slot);
    List<InventorySlot> Slots { get; }
}
