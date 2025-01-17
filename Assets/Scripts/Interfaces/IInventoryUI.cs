using System.Collections.Generic;

public interface IInventoryUI
{
    void UpdateUI(List<InventorySlot> slots);
    void ShowSlotDetails(InventorySlot slot);
    void HideSlotDetails();
}