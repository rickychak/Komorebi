using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Komorebi.Debug
{
    public class DebugDisplayManager: MonoBehaviour
    {
        private Dictionary<string, DebugCategory> _debugCategories = new();
        private TextMeshProUGUI _textMesh;
        private CanvasGroup _canvasGroup;

        public void Awake()
        {
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            
            if (_textMesh == null)
            {
                UnityEngine.Debug.LogError("Cannot find TextMeshPro");
            }
            
            if (_canvasGroup == null)
            {
                UnityEngine.Debug.LogError("Cannot find CanvasGroup");
            }

            UpdateVisibility();
        }
        
        public DebugCategory CreateCategory(string categoryName)
        {
            var category = new DebugCategory(categoryName);
            _debugCategories[categoryName] = category;
            return category;
        }

        private string JoinDebugObjects()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var category in _debugCategories.Values)
            {
                builder.AppendLine($"=== {category.GetCategoryName()} ===");
                foreach (var debugObject in category.GetDebugObjects())
                {
                    builder.AppendLine(debugObject.RetrieveDebugObject());
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        private void Update()
        {
            UpdateVisibility();
            if (!DebugSettings.IsDebugMode) return;
            _textMesh.text = JoinDebugObjects();
        }

        private void UpdateVisibility()
        {
            if (_canvasGroup)
            {
                _canvasGroup.alpha = DebugSettings.IsDebugMode ? 1 : 0;
                _canvasGroup.interactable = DebugSettings.IsDebugMode;
                _canvasGroup.blocksRaycasts = DebugSettings.IsDebugMode;
            }
        }
    }
}
