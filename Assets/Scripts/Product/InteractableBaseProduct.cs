using UnityEngine;

namespace Komorebi.Product
{
    public class InteractableBaseProduct : IInteractable
    {
        private bool isActivated = false;

        public bool IsActivated()
        {
            return isActivated;
        }

        public void Toggle()
        {
            isActivated = !isActivated;
        }

        public void ShowPrompt()
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetGameObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
