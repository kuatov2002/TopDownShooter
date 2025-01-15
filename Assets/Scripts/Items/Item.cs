using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int itemId;
    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int maxStackSize;
    [SerializeField] private bool isStackable;
    public string DisplayName => displayName;
    public bool IsStackable=> isStackable;
    public int ItemId => itemId;

    public virtual void Use()
    {}
}