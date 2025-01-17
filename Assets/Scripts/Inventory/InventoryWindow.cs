using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryWindow : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;

    private List<GameObject> uiSlots = new List<GameObject>();

    private IInventoryManager _inventoryManager; // Reference to the manager through interface
    private SlotInformationPanel slotInformationPanel;

    [Inject]
    public void Construct(IInventoryManager inventoryManager, SlotInformationPanel slotInformationPanel)
    {
        _inventoryManager = inventoryManager;
        this.slotInformationPanel= slotInformationPanel;

        _inventoryManager.OnInventoryChanged += UpdateUI;
    }
    public void ToggleActive()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void UpdateUI()
    {
        // Очищаем старые UI-слоты
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Создаем UI-слоты на основе данных из InventoryManager
        foreach (var slot in _inventoryManager.Slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);

            TextMeshProUGUI text = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = slot.Quantity == 1 ? $"{slot.Item.DisplayName}" : $"{slot.Item.DisplayName} x{slot.Quantity}";
            }


            Transform iconTransform = newSlot.transform.Find("Icon");

            if (iconTransform != null)
            {
                Image icon = iconTransform.GetComponent<Image>();
                if (icon != null)
                {
                    icon.sprite = slot.Item.Icon;
                }
            }

            Button button = newSlot.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnSlotClicked(slot));
            }

        }
    }

    private void OnSlotClicked(InventorySlot slot)
    {
        slotInformationPanel.SetSlot(slot);
        slotInformationPanel.gameObject.SetActive(true);
        //_inventoryManager.UseSlot(slot);
    }

    private void OnDestroy()
    {
        _inventoryManager.OnInventoryChanged -= UpdateUI;
    }
}
