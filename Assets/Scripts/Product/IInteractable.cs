namespace Komorebi.Product
{
    public interface IInteractable
    {
        public bool IsActivated();
        public void Toggle();
        public void Interact();
        public string ShowPrompt();
    }
}