using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    private PlayerController _player;
    private List<Zombie> _enemies;
    private IInventoryManager _inventoryManager;

    private static string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");

    [Inject]
    public void Construct(PlayerController player, [Inject(Id = "Enemies")] List<Zombie> enemies, IInventoryManager inventoryManager)
    {
        _player = player;
        _enemies = enemies;
        _inventoryManager = inventoryManager;
    }

    private void Start()
    {
        LoadGame();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = _player.transform.position,
            playerHealth = _player.CurrentHealth,
            inventorySlots = ConvertInventoryToSaveData(_inventoryManager.Slots),
            enemies = ConvertEnemiesToSaveData(_enemies)
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"Game saved to: {SavePath}");
    }

    public void LoadGame()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            _player.transform.position = saveData.playerPosition;
            _player.CurrentHealth = saveData.playerHealth;

            RestoreInventory(saveData.inventorySlots);
            RestoreEnemies(saveData.enemies);
            Debug.Log("Game loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No save file found.");
        }
    }

    private List<InventorySlotData> ConvertInventoryToSaveData(List<InventorySlot> slots)
    {
        List<InventorySlotData> inventoryData = new();

        foreach (var slot in slots)
        {
            inventoryData.Add(new InventorySlotData
            {
                itemId = slot.Item.ItemId,
                quantity = slot.Quantity
            });
        }

        return inventoryData;
    }

    private List<EnemyData> ConvertEnemiesToSaveData(List<Zombie> enemies)
    {
        List<EnemyData> enemyData = new();

        foreach (var enemy in enemies)
        {
            enemyData.Add(new EnemyData
            {
                position = enemy.transform.position,
                health = enemy.CurrentHealth
            });
        }

        return enemyData;
    }

    private void RestoreInventory(List<InventorySlotData> savedInventory)
    {
        _inventoryManager.Slots.Clear();
        foreach (var slotData in savedInventory)
        {
            Item item = _inventoryManager.GetItemById(slotData.itemId); // Предполагаем, что метод существует
            _inventoryManager.AddItem(item, slotData.quantity);
        }
    }

    private void RestoreEnemies(List<EnemyData> savedEnemies)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (i < savedEnemies.Count)
            {
                _enemies[i].transform.position = savedEnemies[i].position;
                _enemies[i].CurrentHealth = savedEnemies[i].health;
            }
        }
    }
}

[Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public float playerHealth;
    public List<InventorySlotData> inventorySlots;
    public List<EnemyData> enemies;
}

[Serializable]
public class InventorySlotData
{
    public int itemId;
    public int quantity;
}

[Serializable]
public class EnemyData
{
    public Vector3 position;
    public float health;
}
