using System;
using UnityEngine;

public class InventoryController
{
    private GameObject[] _inventory = new GameObject[5];

    private int _currentSlot = 0;

    public InventoryController()
    {
        //read storage
    }

    private void Awake()
    {
        // get all needed dependency
        throw new NotImplementedException();
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
        _currentSlot = index;
        return _inventory[index];
    }

    public void RemoveItemFromInventory()
    {
        _inventory[_currentSlot] = null;
    }
    
}
