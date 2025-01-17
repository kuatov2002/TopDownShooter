using UnityEngine;

public class LootCollector : MonoBehaviour, ILootCollector
{
    [SerializeField] private LayerMask lootLayer; // ���� � ���������� ����
    [SerializeField] private float collectRadius = 2f; // ������ �����

    private IInventoryManager inventoryManager;

    // ������������� ��������� ����� ���������
    public void Initialize(IInventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ��������� �� ������ �� ���� ����
        if ((1 << collision.gameObject.layer & lootLayer) != 0)
        {
            Loot loot = collision.GetComponent<Loot>(); // ��������� ��������� Loot
            if (loot != null)
            {
                // ��������� ������� � ���������
                inventoryManager.AddItem(loot.Item);
                Destroy(collision.gameObject); // ������� ���
            }
        }
    }

    // ��� ������� � ��������� ������ ������ �����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectRadius);
    }
}