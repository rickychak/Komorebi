using Unity.VisualScripting;
using UnityEngine;
using Komorebi.Debug;

namespace Komorebi.Product
{
    public class Door :  BaseEquipment
    { 
        private void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            Animator.SetBool(AnimIsClosed, IsClosed);
            var equipmentCategory = DebugDisplayManager.Instance.CreateCategory("Equipment Status");
            equipmentCategory.AddDebugValue(gameObject.name + ": ", () => IsToggled);
        }
    }    
}


