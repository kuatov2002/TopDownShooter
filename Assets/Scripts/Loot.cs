using UnityEngine;
using Zenject;

public class Loot : MonoBehaviour
{
    [SerializeField] private Item item;
    public Item Item
    {
        get => item;
        set => item = value;
    }
}