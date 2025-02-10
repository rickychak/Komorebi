using UnityEngine;

public interface IInventorySystem
{
    bool CanAddItem();
    bool AddItem(GameObject item);
    event System.Action<GameObject> OnItemAdded;
    bool HasFreeSlot();
    GameObject GetItemAtSlot(int slot);
} 