using System.Text;

namespace Komorebi.Debug
{
    public class DebugObject
    {
        private string _label;
        private object _value;

        public DebugObject(string label, object value)
        {
            this._label = label;
            this._value = value;
        }

        public string RetrieveDebugObject()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_label);
            builder.Append(": ");
            builder.Append(_value);
            return builder.ToString();
        }
    }
}