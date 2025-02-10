using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using Komorebi.Debug;
using Zenject;

namespace Komorebi.Product
{
    public class Fridge : BaseEquipment
    {
        private Animator _animator;
        private IInventorySystem _inventorySystem;
        
        private const string EQUIPMENT_TOGGLE_ANIMATION_CONTROL = "toggle";

        [Inject]
        public void Construct(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void OnInteract()
        {
            base.OnInteract();
            
            if (!_inventorySystem.HasFreeSlot())
            {
                UnityEngine.Debug.Log("Cannot get donut - inventory is full!");
                return;
            }
            
            GameObject donut = DonutPool.DonutPoolInstance.GetDonut();
            if (donut != null)
            {
                NotifyItemProduced(donut);
            }
        }

        public override void TriggerAnimation()
        {
            UnityEngine.Debug.Log("Fridge Triggered");
        }
    }
}