namespace Komorebi.Product
{
    public class BaseEquipment: InteractableItem
    {
        public override void Toggle()
        {
            
            UnityEngine.Debug.Log(gameObject.name + " is toggled");
        }

        public override void TriggerAnimation()
        {
            
        }
    }
}