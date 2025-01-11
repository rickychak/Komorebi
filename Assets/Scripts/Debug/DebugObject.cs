using System;
using System.Text;
using Vector3 = UnityEngine.Vector3;

namespace Komorebi.Debug
{
    public class DebugObject
    {
        private string _label;
        private Func<object> _valueGetter;

        // For reference types and structs that need to be monitored
        public static DebugObject Create<T>(string label, Func<T> getter)
        {
            return new DebugObject(label, () => getter());
        }

        private DebugObject(string label, Func<object> getter)
        {
            _label = label;
            _valueGetter = getter;
        }

        public string RetrieveDebugObject()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_label);
            builder.Append(": ");
            builder.Append(_valueGetter());
            return builder.ToString();
        }
    }
}