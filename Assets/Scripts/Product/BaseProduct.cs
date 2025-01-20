using Komorebi.Product;
using UnityEngine;

public class BaseProduct : MonoBehaviour, IInteractable
{
    protected InventoryController _inventoryController;

    protected virtual void Awake()
    {
        _inventoryController = new InventoryController();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public bool IsPromptShown { get; set; }
    public bool IsPickedUp { get; set; }

    public void ShowPrompt()
    {
        
    }

    public virtual void Toggle()
    {
        if (_inventoryController.AddItemToInventory(gameObject))
        {
            IsPickedUp = true;
            gameObject.SetActive(false);
        }
    }

    public void TriggerAnimation()
    {
        
    }
}
