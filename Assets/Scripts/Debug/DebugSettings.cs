namespace Komorebi.Debug
{
    public static class DebugSettings
    {
        public static bool IsDebugMode { get; private set; }

        public static void EnableDebugMode()
        {
            IsDebugMode = true;
        }

        public static void DisableDebugMode()
        {
            IsDebugMode = false;
        }

        public static void ToggleDebugMode()
        {
            IsDebugMode = !IsDebugMode;
        }
    }
} 