using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;


    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    public List<InventorySlot> Slots=> slots;

    public Item testitem;
    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AddItem(testitem,2);
        AddItem(testitem);
        AddItem(testitem);
        AddItem(testitem);
        AddItem(testitem);
        AddItem(testitem);

    }



    void Start()
    {

    }


    // Метод для добавления предмета в слот
    public void AddItem(Item newItem, int quantity = 1)
    {
        // Проверяем, есть ли уже слот с таким предметом и он может быть сложен (stackable)
        InventorySlot existingSlot = slots.Find(slot => slot.Item.ItemId == newItem.ItemId && slot.Item.IsStackable);

        if (existingSlot != null)
        {
            // Если слот найден, добавляем количество
            existingSlot.AddItem(newItem, quantity);
        }
        else
        {
            // Если слота с таким предметом нет, создаем новый
            InventorySlot newSlot = new InventorySlot(newItem, quantity);
            slots.Add(newSlot);
        }
    }

    // Метод для использования предмета из слота
    public void UseSlot(InventorySlot slot)
    {
        if (slot.Item != null)
        {
            slot.UseItem(); // Вызываем метод UseItem() у слота, который использует предмет
        }

        // Если количество предметов в слоте равно 0, удаляем слот из списка
        if (slot.Quantity == 0)
        {
            slots.Remove(slot);
        }
    }

}
