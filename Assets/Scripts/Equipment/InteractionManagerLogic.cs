using System.Collections.Generic;
using UnityEngine;

// Pure C# class containing testable logic
public class InteractionManagerLogic
{
    public void RegisterItem(List<IInteractable> items, IInteractable item)
    {
        if (!items.Contains(item))
            items.Add(item);
    }

    public void UnregisterItem(List<IInteractable> items, IInteractable item)
    {
        items.Remove(item);
    }

    public void CheckInteractions(List<IInteractable> items, Vector3 playerPos, float interactionDistance)
    {
        foreach (var item in items)
        {
            if (item == null) continue;
            
            float distance = Vector3.Distance(playerPos, item.Position);
            item.UpdateUIVisibility(distance <= interactionDistance);
        }
    }
} 