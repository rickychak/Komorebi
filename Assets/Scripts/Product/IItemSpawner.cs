using UnityEngine;

public interface IItemSpawner
{
    GameObject SpawnItem();
    void ReturnItem(GameObject item);
} 