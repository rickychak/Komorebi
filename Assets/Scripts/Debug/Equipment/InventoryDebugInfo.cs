using UnityEngine;
using Zenject;

namespace Komorebi.Debug.Equipment
{
    public class InventoryDebugInfo : IInitializable
    {
        private readonly IInventorySystem _inventorySystem;

        [Inject]
        public InventoryDebugInfo(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }

        public void Initialize()
        {
            RegisterDebugInfo();
        }

        private void RegisterDebugInfo()
        {
            var debugManager = DebugDisplayManager.Instance;
            var category = debugManager.CreateCategory("Inventory");
            
            category.AddDebugValue("Has Free Slot", () => _inventorySystem.HasFreeSlot());
            category.AddDebugValue("Occupied Slots", () => GetOccupiedSlotCount());
        }

        private int GetOccupiedSlotCount()
        {
            int count = 0;
            for (int i = 0; i < 4; i++)  // Assuming max slots is 4
            {
                if (_inventorySystem.GetItemAtSlot(i) != null)
                    count++;
            }
            return count;
        }
    }
} 