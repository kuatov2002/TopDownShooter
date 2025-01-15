using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]private GameObject inventoryPanel; // ������, ���� ����� ����������� �����
    public GameObject slotPrefab;     // ������ ����� (������)

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
        // ������ ������� ����� ����� �� ������ ���������� �� InventoryManager
        foreach (InventorySlot slot in InventoryManager.instance.Slots)
        {
            // ������� ����� ���� �� ������� � ������ ��� �������� �������� ��� ������
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
            newSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.Item.DisplayName;
        }
    }
}
