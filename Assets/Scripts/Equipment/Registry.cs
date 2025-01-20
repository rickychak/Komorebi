using DG.Tweening;
using Komorebi.Debug;
using UnityEngine;

namespace Komorebi.Product
{
    public class Registry:  BaseEquipment
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