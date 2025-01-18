using UnityEngine;

namespace Komorebi.Product
{
    public interface IInteractable
    {
        public bool IsActivated();
        public void Toggle();
        public void Interact();
        public void ShowPrompt();
        GameObject GetGameObject();
    }
}