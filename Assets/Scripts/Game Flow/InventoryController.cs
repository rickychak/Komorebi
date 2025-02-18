using System;
using Komorebi.Debug;
using UnityEngine;

public class InventoryController : IDebuggable
{
    private GameObject[] _inventory = new GameObject[5];
    private DebugDisplayManager _debugDisplayManager;
    
    public int CurrentSlot { get; set; } = 0;
    
    private CharacterDebugInfo _debugInfo;
    private DebugRegistration _debugRegistration;

    public InventoryController()
    {
        _debugDisplayManager = DebugDisplayManager.Instance;
        var inventoryCategory = _debugDisplayManager.CreateCategory("Inventory");
        inventoryCategory.AddDebugValue("Inventory: ", () =>
        {
            var slot1 = GetItemFromInventory(0)?.name;
            var slot2 = GetItemFromInventory(1)?.name;
            var slot3 = GetItemFromInventory(2)?.name;
            var slot4 = GetItemFromInventory(3)?.name;
            var slot5 = GetItemFromInventory(4)?.name;

            return $"Slot1: {slot1} slot2: {slot2} slot3: {slot3} slot4: {slot4} slot5: {slot5}";
        });

        _debugRegistration = new DebugRegistration();
        _debugRegistration.RegisterComponent(this, "Character.Inventory");
    }

    public void RegisterDebugValues(DebugCategory category)
    {
        category.AddDebugValue("Current Slot", () => CurrentSlot);
        category.AddDebugValue("Inventory Contents", () =>
        {
            var slot1 = GetItemFromInventory(0)?.name ?? "empty";
            var slot2 = GetItemFromInventory(1)?.name ?? "empty";
            var slot3 = GetItemFromInventory(2)?.name ?? "empty";
            var slot4 = GetItemFromInventory(3)?.name ?? "empty";
            var slot5 = GetItemFromInventory(4)?.name ?? "empty";

            return $"[{slot1}] [{slot2}] [{slot3}] [{slot4}] [{slot5}]";
        });
    }

    private void SaveToScriptableObject()
    {
        
    }

    private void LoadFromScriptableObject()
    {
        
    }

    public bool AddItemToInventory(GameObject gameObject)
    {
        for (int i = 0; i < _inventory.Length; i++)
        {
            if (!_inventory[i])
            {
                _inventory[i] = gameObject;
                return true;
            }
        }
        
        return false;
        
        //callback fail
    }

    public GameObject GetItemFromInventory(int index)
    {
        return _inventory[index];
    }

    public GameObject RemoveItemFromInventory()
    {
        GameObject itemToRemove = _inventory[CurrentSlot];
        if (itemToRemove)
        {
            itemToRemove.SetActive(true);
        }
        _inventory[CurrentSlot] = null;
        return itemToRemove;
    }
    
    
}
