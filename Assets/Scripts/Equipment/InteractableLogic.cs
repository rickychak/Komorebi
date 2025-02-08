// Pure C# class that contains the testable logic

using DG.Tweening;
using UnityEngine;

public class InteractableLogic
{
    private readonly SpriteRenderer _spriteRenderer;

    public InteractableLogic(GameObject uiElement)
    {
        if (!uiElement) return;
        _spriteRenderer = uiElement.GetComponent<SpriteRenderer>();
    }

    public void UpdateUIVisibility(bool visible)
    {
        if (!_spriteRenderer) return;
        if (visible)
        {
            _spriteRenderer.DOFade(1f, 0.5f);
            return;
        }
        _spriteRenderer.DOFade(0f, 0.5f);
        
    }
} 