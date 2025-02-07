using UnityEngine;

public interface IInteractable
{
    Vector3 Position { get; }
    void UpdateUIVisibility(bool visible);
} 