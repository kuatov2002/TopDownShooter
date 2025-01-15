using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Item item;                      // ������� � �����
    public Item Item=> item;

    public int quantity=0;                   // ���������� ��������� � �����
    public TextMeshProUGUI quantityText;   // ����� ��� ����������� ���������� ���������
    //public Image itemIcon;                 // ������ ��������

    public InventorySlot(Item item, int quantity = 1)
    {
        this.item = item;
        this.quantity = quantity;
    }
    public void UpdateSlot()
    {
        if (item != null)
        {
            // ��������� ������ � ����������
            //itemIcon.sprite = item.Icon;
            //itemIcon.enabled = true;
            quantityText.text = quantity > 1 ? quantity.ToString() : "";
        }
        else
        {
            // ���� ���� ������, �������� ������ � �����
            //itemIcon.sprite = null;
            //itemIcon.enabled = false;
            quantityText.text = "";
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            quantity--;

            if (quantity <= 0)
            {
                DeleteItem();
            }
            else
            {
                UpdateSlot();
            }
        }
    }

    public void DeleteItem()
    {
        item = null;
        quantity = 0;
        UpdateSlot();
    }

    public void AddItem(Item newItem, int amount=1)
    {
        item = newItem;
        quantity += amount;
        UpdateSlot();
    }
}