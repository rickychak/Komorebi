using System.Collections.Generic;

namespace Komorebi.Debug
{
    public class DebugCategory
    {
        private string _categoryName;
        private List<DebugObject> _debugObjects = new();

        public DebugCategory(string categoryName)
        {
            _categoryName = categoryName;
        }

        public void AddDebugValue<T>(string label, System.Func<T> getter)
        {
            _debugObjects.Add(DebugObject.Create(label, getter));
        }

        public IEnumerable<DebugObject> GetDebugObjects()
        {
            return _debugObjects;
        }

        public string GetCategoryName()
        {
            return _categoryName;
        }
    }
} 