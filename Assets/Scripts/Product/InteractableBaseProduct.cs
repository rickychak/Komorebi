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

        public string ShowPrompt()
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}
