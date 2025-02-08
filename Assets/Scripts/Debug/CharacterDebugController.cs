using UnityEngine;

namespace Komorebi.Debug
{
    public class CharacterDebugController : MonoBehaviour
    {
        private void Awake()
        {
            var debugManager = DebugDisplayManager.Instance;
            
            // Create a group for character-related categories
            debugManager.CreateCategoryGroup("Character", 
                "Character.Input",
                "Character.Movement",
                "Character.Velocity",
                "Character.BrakeForces",
                "Character.Interact",
                "Character.Inventory"
            );
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                DebugDisplayManager.Instance.ToggleCategory("Character");
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                DebugDisplayManager.Instance.ToggleCategory("Character.Input");
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                DebugDisplayManager.Instance.ToggleCategory("Character.Movement");
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                DebugDisplayManager.Instance.ToggleCategory("Character.Velocity");
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                DebugDisplayManager.Instance.ToggleCategory("Character.BrakeForces");
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                DebugDisplayManager.Instance.ToggleCategory("Character.Interact");
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                DebugDisplayManager.Instance.ToggleCategory("Character.Inventory");
            }
        }
    }
} 