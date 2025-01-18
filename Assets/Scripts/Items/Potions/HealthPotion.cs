using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New Health Potion", menuName = "Inventory/Potion/HealthPotion")]
public class HealthPotion : Potion
{
    [SerializeField] private int healAmount;



    private PlayerController player;

    public override void Use()
    {
        player = FindObjectOfType<PlayerController>();
        Debug.Log($"Used health potion. Restored {healAmount} HP.");
        player.Heal(healAmount);//пока временно он будет просто хилить, потом надо добавить чтобы он хилил со временем
    }
}