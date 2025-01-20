using UnityEngine;

namespace Komorebi.Product
{
    public interface IInteractable
    {
        public bool IsPromptShown { get; set; }
        public void Toggle();
        public void TriggerAnimation();
        public void ShowPrompt();
        GameObject GetGameObject();
    }
}