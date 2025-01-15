using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]private GameObject inventoryPanel; // Панель, куда будут добавляться слоты
    public GameObject slotPrefab;     // Префаб слота (кнопка)

    public void ToggleActive()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    void Start()
    {
        DrawSlots();
        print("drawed");
    }
    private void DrawSlots()
    {
        // Теперь создаем новые слоты на основе информации из InventoryManager
        foreach (InventorySlot slot in InventoryManager.instance.Slots)
        {
            // Создаем новый слот из префаба и делаем его дочерним объектом для панели
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
            newSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.Item.DisplayName;
        }
    }
}
