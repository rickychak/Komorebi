using System;
using UnityEngine;
using Komorebi.Debug;

namespace Product
{
    public class Donut: BaseProduct
    {
        private float _progress { get; set; }

        private void Start()
        {
            var foodCategory = DebugDisplayManager.Instance.CreateCategory("Product");
            foodCategory.AddDebugValue(name +": ", () => IsPickedUp);
        }
    }
}