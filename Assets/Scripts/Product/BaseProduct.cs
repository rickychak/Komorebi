using System;
using Komorebi.Product;
using UnityEngine;

public class BaseProduct : MonoBehaviour, IInteractable
{
    private SpriteRenderer _spriteRenderer;

    public float Progress { set; get; } = 0.0f; 

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TriggerProgress(float interval, bool increase)
    {
        if (increase)
        {
            Progress += interval;
            return;
        }
        Progress -= interval;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public bool IsPromptShown { get; set; }
    public bool IsPickedUp { get; set; }

    public void ShowUI()
    {
        
    }

    public virtual void Toggle()
    {
        IsPickedUp = true;
        gameObject.SetActive(false);
    }

    public void TriggerAnimation()
    {
        
    }
}
