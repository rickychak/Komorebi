using System;
using Komorebi.Product;
using UnityEngine;

public class BaseProduct : InteractableItem
{
    private SpriteRenderer _spriteRenderer;

    public float Progress { set; get; } = 0.0f; 

    public void TriggerProgress(float interval, bool increase)
    {
        if (increase)
        {
            Progress += interval;
            return;
        }
        Progress -= interval;
    }

    public bool IsPickedUp { get; set; }

    public void ShowUI()
    {
        
    }

    public void Toggle()
    {
        IsPickedUp = true;
        gameObject.SetActive(false);
    }

    public void TriggerAnimation()
    {
        
    }
}
