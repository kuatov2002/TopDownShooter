using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    [SerializeField] private float duration;
    [SerializeField] private float effectRate;

    public override void Use() {}
}