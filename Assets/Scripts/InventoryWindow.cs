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
        // Очищаем старые UI слоты
        foreach (GameObject slot in uiSlots)
        {
            Destroy(slot);
        }
        uiSlots.Clear();

        // Создаем новые UI слоты
        for (int i = 0; i < InventoryManager.instance.Slots.Count; i++)
        {
            InventorySlot slot = InventoryManager.instance.Slots[i];
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);

            // Устанавливаем текст и обрабатываем клик
            TextMeshProUGUI text = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = $"{slot.Item.DisplayName} x{slot.Quantity}";
            }

            // Добавляем обработчик нажатия на кнопку
            Button button = newSlot.GetComponent<Button>();
            if (button != null)
            {
                int index = i; // Копируем индекс для замыкания
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
            UpdateUI();      // Обновляем интерфейс
        }
    }
}
