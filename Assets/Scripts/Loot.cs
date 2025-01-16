using UnityEngine;
using Zenject;

public class Loot : MonoBehaviour, IInteractable
{
    [SerializeField] private Item item;
    private IInventoryManager _inventoryManager;

    // Scene injection will automatically call this method after the object is created
    [Inject]
    public void Construct(IInventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;

        print(item.DisplayName);
    }

    public void Interact()
    {
        _inventoryManager.AddItem(item);
        Destroy(gameObject);
    }
}