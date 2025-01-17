using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class InventoryWindow : MonoBehaviour, IInventoryUI
{
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private SlotInformationPanel slotDetailsPanel;

    private readonly List<GameObject> uiSlots = new();
    private IInventoryManager inventoryManager;

    // Внедрение IInventoryManager через конструктор
    [Inject]
    public void Construct(IInventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
        this.inventoryManager.OnInventoryChanged += UpdateUI; // Подписываемся на изменения инвентаря
    }

    private void OnDestroy()
    {
        if (inventoryManager != null)
        {
            this.inventoryManager.OnInventoryChanged -= UpdateUI; // Отписываемся от события при уничтожении
        }
    }

    // Метод для обновления UI при изменении инвентаря
    public void UpdateUI(List<InventorySlot> slots)
    {
        // Очистка старого UI через объектный пул
        foreach (var slot in uiSlots)
        {
            slot.SetActive(false);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            GameObject uiSlot;
            if (i >= uiSlots.Count)
            {
                uiSlot = Instantiate(slotPrefab, inventoryPanel);
                uiSlots.Add(uiSlot);
            }
            else
            {
                uiSlot = uiSlots[i];
                uiSlot.SetActive(true);
            }

            var slot = slots[i];
            var text = uiSlot.GetComponentInChildren<TextMeshProUGUI>();
            var icon = uiSlot.transform.Find("Icon")?.GetComponent<Image>();

            if (text != null) text.text = slot.Quantity > 1 ? $"{slot.Item.DisplayName} x{slot.Quantity}" : slot.Item.DisplayName;
            if (icon != null) icon.sprite = slot.Item.Icon;

            var button = uiSlot.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ShowSlotDetails(slot));
            }
        }
    }

    public void ToggleActive()
    {
        inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
    }

    public void ShowSlotDetails(InventorySlot slot)
    {
        slotDetailsPanel.SetSlot(slot);
        slotDetailsPanel.gameObject.SetActive(true);
    }

    public void HideSlotDetails()
    {
        slotDetailsPanel.gameObject.SetActive(false);
    }
}
