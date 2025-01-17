using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SlotInformationPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Button useButton;
    [SerializeField] private Button deleteButton;

    private InventorySlot currentSlot;

    private IInventoryManager inventoryManager;

    // Внедрение IInventoryManager через конструктор
    [Inject]
    public void Construct(IInventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
    }

    public void SetSlot(InventorySlot slot)
    {
        icon.sprite = slot.Item.Icon;
        description.text = slot.Item.Description;
        name.text = slot.Item.DisplayName;

        currentSlot = slot;

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() => UseSlot());

        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() => DeleteSlot());
    }

    private void UseSlot()
    {
        inventoryManager.UseSlot(currentSlot);
        gameObject.SetActive(false);
    }

    private void DeleteSlot()
    {
        inventoryManager.RemoveSlot(currentSlot);
        gameObject.SetActive(false);
    }
}