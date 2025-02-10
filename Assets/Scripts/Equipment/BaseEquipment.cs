using UnityEngine;

namespace Komorebi.Product
{
    public class BaseEquipment : InteractableItem, IEquipmentInteraction
    {
        private Animator _animator;
        
        private const string EQUIPMENT_TOGGLE_ANIMATION_CONTROL = "toggle";

        public event System.Action<GameObject> OnItemProduced;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void OnInteract()
        {
            Toggle();
            TriggerAnimation();
        }

        protected void NotifyItemProduced(GameObject item)
        {
            OnItemProduced?.Invoke(item);
        }

        public override void Toggle()
        {
            if (!_animator) return;
            _animator.SetTrigger(EQUIPMENT_TOGGLE_ANIMATION_CONTROL);
        }

        public override void TriggerAnimation()
        {
            UnityEngine.Debug.Log("Equipment Triggered");
        }
    }
}