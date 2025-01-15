using UnityEngine;

[CreateAssetMenu(fileName = "New Health Potion", menuName = "Inventory/Potion/HealthPotion")]
public class HealthPotion : Potion
{
    [SerializeField] private int healAmount;

    public override void Use()
    {
        Debug.Log($"Used health potion. Restored {healAmount} HP.");
    }
}