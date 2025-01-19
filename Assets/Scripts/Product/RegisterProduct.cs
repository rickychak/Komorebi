using DG.Tweening;
using UnityEngine;

namespace Komorebi.Product
{
    public class RegisterProduct:  MonoBehaviour, IInteractable
    {
        private bool _isActivated = false;
        private bool _isClosed = true;
       
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponentInChildren<Animator>();
            
        }

        private void Start()
        {
            // _animator.SetBool(IsClosed, _isClosed);
        }

        public bool IsActivated()
        {
            return _isActivated;
        }

        public void Toggle()
        {
            
        }

        public void ShowPrompt()
        {
            _ = _isActivated ? _spriteRenderer.DOFade(0, 0.5f) : _spriteRenderer.DOFade(1, 0.5f);
            _isActivated = !_isActivated;
            
        }

        public void Interact()
        {
            // _isClosed = !_isClosed;
            // _animator.SetBool(IsClosed, _isClosed);
            // _animator.SetTrigger(AnimToggle);
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}