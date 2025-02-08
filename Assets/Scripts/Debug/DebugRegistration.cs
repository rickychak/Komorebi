using System.Collections.Generic;
using UnityEngine;

namespace Komorebi.Debug
{
    public class DebugRegistration
    {
        private Dictionary<string, DebugCategory> _categories = new();
        private DebugDisplayManager _debugManager;

        public DebugRegistration()
        {
            _debugManager = DebugDisplayManager.Instance;
        }

        public DebugCategory GetOrCreateCategory(string categoryName)
        {
            if (!_categories.ContainsKey(categoryName))
            {
                _categories[categoryName] = _debugManager.CreateCategory(categoryName);
            }
            return _categories[categoryName];
        }

        public void RegisterComponent<T>(T component, string categoryName) where T : IDebuggable
        {
            var category = GetOrCreateCategory(categoryName);
            component.RegisterDebugValues(category);
        }
    }
} 