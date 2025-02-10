using DefaultNamespace;
using UnityEngine;

// MonoBehaviour wrapper that implements IInteractable
public abstract class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject itemUI;
    private InteractableLogic logic;

    public Vector3 Position => transform.position;

    
    private void Start()
    {
        logic = new InteractableLogic(itemUI);
        InteractionManager.Instance.RegisterItem(this);
    }

    private void OnDisable()
    {
        InteractionManager.Instance.UnregisterItem(this);
    }

    public virtual void UpdateUIVisibility(bool visible)
    {
        logic.UpdateUIVisibility(visible);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public abstract void Toggle();
    public abstract void TriggerAnimation();

} 