using UnityEngine;

namespace DefaultNamespace
{
    public class UIGameObject: IUIElement
    {
        [SerializeField] private GameObject gameObject;
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}