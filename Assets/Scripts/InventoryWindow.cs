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

    [Inject]
    public void Construct(IInventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
    }
    public void ToggleActive()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        // ������� ������ UI-�����
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // ������� UI-����� �� ������ ������ �� InventoryManager
        foreach (var slot in _inventoryManager.Slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);

            TextMeshProUGUI text = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = $"{slot.Item.DisplayName} x{slot.Quantity}";
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
        _inventoryManager.UseSlot(slot);
    }
}
