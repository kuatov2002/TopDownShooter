using System;
using System.Collections.Generic;

public interface IInventoryManager
{
    event Action<List<InventorySlot>> OnInventoryChanged;

    List<InventorySlot> Slots { get; }

    void AddItem(Item newItem, int quantity = 1);

    void UseSlot(InventorySlot slot);

    void RemoveSlot(InventorySlot slot);

    bool CanAddItem(Item newItem);

    int GetItemCount(Item item);

    Item GetItemById(int itemId);
}