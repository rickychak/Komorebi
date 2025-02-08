using System;

namespace Komorebi.Debug
{
    public interface IDebuggable
    {
        void RegisterDebugValues(DebugCategory category);
    }
} 