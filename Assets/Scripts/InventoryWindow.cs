using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    public static InventoryWindow instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;

    private List<GameObject> uiSlots = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleActive()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        UpdateUI();
    }

    public void UpdateUI()
    {
        // ������� ������ UI �����
        foreach (GameObject slot in uiSlots)
        {
            Destroy(slot);
        }
        uiSlots.Clear();

        // ������� ����� UI �����
        for (int i = 0; i < InventoryManager.instance.Slots.Count; i++)
        {
            InventorySlot slot = InventoryManager.instance.Slots[i];
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);

            // ������������� ����� � ������������ ����
            TextMeshProUGUI text = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = $"{slot.Item.DisplayName} x{slot.Quantity}";
            }

            // ��������� ���������� ������� �� ������
            Button button = newSlot.GetComponent<Button>();
            if (button != null)
            {
                int index = i; // �������� ������ ��� ���������
                button.onClick.AddListener(() => OnSlotClicked(index));
            }

            uiSlots.Add(newSlot);
        }
    }

    private void OnSlotClicked(int slotIndex)
    {
        InventorySlot slot = InventoryManager.instance.Slots[slotIndex];
        if (slot.Item != null)
        {
            InventoryManager.instance.UseSlot(slot);
            UpdateUI();      // ��������� ���������
        }
    }
}
