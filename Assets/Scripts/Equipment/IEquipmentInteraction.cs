using System;
using UnityEngine;

public interface IEquipmentInteraction
{
    void OnInteract();
    event Action<GameObject> OnItemProduced;
} 