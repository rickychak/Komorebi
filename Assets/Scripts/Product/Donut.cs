using System;
using UnityEngine;
using Komorebi.Debug;

namespace Product
{
    public class Donut: BaseProduct
    {
        private float _progress { get; set; } = 0f;
        

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            var foodCategory = DebugDisplayManager.Instance.CreateCategory("Product");
            foodCategory.AddDebugValue(name +": ", () => IsPickedUp);
        }
    }
}