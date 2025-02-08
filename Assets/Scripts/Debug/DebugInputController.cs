using UnityEngine;

namespace Komorebi.Debug
{
    public class DebugInputController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                DebugDisplayManager.Instance.ToggleCategory("Input");
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                DebugDisplayManager.Instance.ToggleCategory("Movement");
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                DebugDisplayManager.Instance.ToggleCategory("Velocity");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                DebugDisplayManager.Instance.ToggleCategory("Brake Forces");
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                DebugDisplayManager.Instance.ToggleCategory("Interact");
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                DebugDisplayManager.Instance.HideAllCategories();
            }
            
            // Toggle debug mode with backtick/tilde key
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                DebugSettings.ToggleDebugMode();
            }
        }
    }
} 