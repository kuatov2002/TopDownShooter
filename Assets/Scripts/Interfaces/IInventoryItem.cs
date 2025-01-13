using UnityEngine;

public interface IInventoryItem
{
    string ItemId { get; }
    string DisplayName { get; }
    Sprite Icon { get; }
    int MaxStackSize { get; }
    bool IsStackable { get; }
}