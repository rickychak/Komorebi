using UnityEngine;
using System;

namespace Komorebi.Product
{
    public class InventoryManager : MonoBehaviour, IInventorySystem
    {
        [SerializeField] private int maxSlots = 4;
        private GameObject[] _inventory;
        
        public event Action<GameObject> OnItemStored;
        public event Action<int> OnItemRemoved;
        public event Action OnInventoryFull;

        private void Awake()
        {
            _inventory = new GameObject[maxSlots];
        }

        public bool HasFreeSlot()
        {
            for (int i = 0; i < _inventory.Length; i++)
            {
                if (_inventory[i] == null)
                    return true;
            }
            return false;
        }

        public bool TryStoreItem(GameObject item)
        {
            for (int i = 0; i < _inventory.Length; i++)
            {
                if (_inventory[i] == null)
                {
                    _inventory[i] = item;
                    OnItemStored?.Invoke(item);
                    return true;
                }
            }
            OnInventoryFull?.Invoke();
            return false;
        }

        public GameObject GetItemAtSlot(int slot)
        {
            if (slot >= 0 && slot < _inventory.Length)
            {
                return _inventory[slot];
            }
            return null;
        }

        public void RemoveItemAtSlot(int slot)
        {
            if (slot >= 0 && slot < _inventory.Length)
            {
                _inventory[slot] = null;
                OnItemRemoved?.Invoke(slot);
            }
        }
    }
} 