using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, IInteractable
{
    [SerializeField] private Item item;

    public void Interact()
    {
        InventoryManager.instance.AddItem(item);
        Destroy(gameObject);
        InventoryWindow.instance.UpdateUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
