using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Komorebi.Debug
{
    public class DebugDisplayManager : MonoBehaviour
    {
        private Dictionary<string, DebugCategory> _debugCategories = new();
        private TextMeshProUGUI _textMesh;
        private CanvasGroup _canvasGroup;
        private HashSet<string> _visibleCategories = new();
        private Dictionary<string, HashSet<string>> _categoryGroups = new();

        // Singleton instance
        private static DebugDisplayManager _instance;
        public static DebugDisplayManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<DebugDisplayManager>();
                    if (_instance == null)
                    {
                        UnityEngine.Debug.LogError("No DebugDisplayManager found in scene!");
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            // Ensure only one instance exists
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            
            // Initialize components
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
            
            if (_debugCategories.ContainsKey(categoryName))
            {
                return _debugCategories[categoryName];
            }
            var category = new DebugCategory(categoryName);
            _debugCategories[categoryName] = category;
            return category;
        }

        public void ToggleCategory(string categoryName)
        {
            if (_visibleCategories.Contains(categoryName))
            {
                _visibleCategories.Remove(categoryName);
            }
            else
            {
                _visibleCategories.Add(categoryName);
            }
        }

        public void HideAllCategories()
        {
            _visibleCategories.Clear();
        }

        public void CreateCategoryGroup(string groupName, params string[] categories)
        {
            if (!_categoryGroups.ContainsKey(groupName))
            {
                _categoryGroups[groupName] = new HashSet<string>();
            }
            foreach (var category in categories)
            {
                _categoryGroups[groupName].Add(category);
            }
        }

        private string JoinDebugObjects()
        {
            StringBuilder builder = new StringBuilder();
            
            // If a visible category is a group, show all its subcategories
            foreach (var visibleCategory in _visibleCategories)
            {
                if (_categoryGroups.ContainsKey(visibleCategory))
                {
                    builder.AppendLine($"=== {visibleCategory} ===");
                    foreach (var subcategory in _categoryGroups[visibleCategory])
                    {
                        if (_debugCategories.ContainsKey(subcategory))
                        {
                            var category = _debugCategories[subcategory];
                            builder.AppendLine($"--- {category.GetCategoryName()} ---");
                            foreach (var debugObject in category.GetDebugObjects())
                            {
                                builder.AppendLine(debugObject.RetrieveDebugObject());
                            }
                            builder.AppendLine();
                        }
                    }
                }
                // If it's a regular category, show it normally
                else if (_debugCategories.ContainsKey(visibleCategory))
                {
                    var category = _debugCategories[visibleCategory];
                    builder.AppendLine($"=== {category.GetCategoryName()} ===");
                    foreach (var debugObject in category.GetDebugObjects())
                    {
                        builder.AppendLine(debugObject.RetrieveDebugObject());
                    }
                    builder.AppendLine();
                }
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
