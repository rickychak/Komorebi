using UnityEngine;
using DG.Tweening;

namespace Komorebi.Product
{
    public abstract class BaseEquipment: MonoBehaviour, IInteractable
    {
        
        public static readonly int AnimIsClosed = Animator.StringToHash("isClosed");
        public static readonly int AnimToggle = Animator.StringToHash("toggle");
        
        public bool IsPromptShown { get; set; } = false;
        public bool IsClosed { get; set; } = true;
        public bool IsToggled { get; set; } = false;
        public SpriteRenderer SpriteRenderer { get; set; }
        public Animator Animator { get; set; }
        
        
        public void Toggle()
        {
            IsToggled = !IsToggled;
        }

        public void ShowPrompt()
        {
            _ = IsPromptShown ? SpriteRenderer.DOFade(0, 0.5f) : SpriteRenderer.DOFade(1, 0.5f);
            IsPromptShown = !IsPromptShown;
            
        }

        public void TriggerAnimation()
        {
            IsClosed = !IsClosed;
            Animator.SetBool(AnimIsClosed, IsClosed);
            Animator.SetTrigger(AnimToggle);
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}