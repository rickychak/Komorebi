using UnityEngine;

public class EquipmentInteractionMediator
{
    private readonly IItemSpawner _itemSpawner;
    private readonly IInventorySystem _inventorySystem;

    public void HandleEquipmentInteraction(IEquipmentInteraction equipment)
    {
        equipment.OnItemProduced += (item) =>
        {
            if (_inventorySystem.CanAddItem())
            {
                _inventorySystem.AddItem(item);
            }
            else
            {
                _itemSpawner.ReturnItem(item);
            }
        };
    }
} 