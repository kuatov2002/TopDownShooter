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


    private IInventoryManager _inventoryManager;

    [Inject]
    public void Construct(IInventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
    }
    public void SetSlot(InventorySlot slot)
    {
        icon.sprite = slot.Item.Icon;
        description.text= slot.Item.Description;
        name.text = slot.Item.DisplayName;

        currentSlot=slot;

        useButton.onClick.AddListener(SlotClicked);
        deleteButton.onClick.AddListener(DeleteSlot);
    }


    private void SlotClicked()
    {
        _inventoryManager.UseSlot(currentSlot);
        gameObject.SetActive(false);
    }
    private void DeleteSlot()
    {
        if (currentSlot != null)
        {
            _inventoryManager.RemoveSlot(currentSlot);
            gameObject.SetActive(false); // Закрываем панель
        }
    }
}
