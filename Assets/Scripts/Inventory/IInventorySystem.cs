using UnityEngine;

namespace Komorebi.Product
{
    public interface IInventorySystem
    {
        bool HasFreeSlot();
        bool TryStoreItem(GameObject item);
        GameObject GetItemAtSlot(int slot);
        void RemoveItemAtSlot(int slot);
    }
} 