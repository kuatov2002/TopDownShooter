using UnityEngine;

public class InventorySlot:MonoBehaviour
{
    [SerializeField] private Item item;
    public Item Item => item;
    [SerializeField] private int quantity = 0;
    public int Quantity => quantity;

    // Since this is no longer a MonoBehaviour, we can keep the constructor
    public InventorySlot(Item item, int quantity = 1)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            quantity--;
            if (quantity <= 0)
            {
                DeleteItem();
            }
        }
    }

    public void DeleteItem()
    {
        int slotIndex = InventoryManager.instance.Slots.IndexOf(this);
        if (slotIndex != -1)
        {
            InventoryManager.instance.Slots.RemoveAt(slotIndex);
        }
        item = null;
        quantity = 0;
        InventoryWindow.instance.UpdateUI();
    }

    public void AddItem(Item newItem, int amount = 1)
    {
        item = newItem;
        quantity += amount;
    }

    public override string ToString()
    {
        return item != null ? $"{item.DisplayName} x{quantity}" : "Empty Slot";
    }
}