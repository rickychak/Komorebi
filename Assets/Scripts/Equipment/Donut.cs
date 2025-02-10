using UnityEngine;
using Zenject;

namespace Komorebi.Product
{
    public class Donut : InteractableItem
    {
        private IInventorySystem _inventorySystem;

        [Inject]
        public void Construct(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }

        public void OnInteract()
        {
            // Store donut in inventory
            if (_inventorySystem.TryStoreItem(gameObject))
            {
                // Disable the donut GameObject after storing
                gameObject.SetActive(false);
            }
            else
            {
                UnityEngine.Debug.Log("Cannot store donut - inventory is full!");
            }
        }

        public override void Toggle()
        {
            // Implementation for any toggle behavior
        }

        public override void TriggerAnimation()
        {
            // Implementation for any animation triggers
        }
    }
} 