using UnityEngine;

namespace Komorebi.Debug
{
    public class CharacterDebugInfo
    {
        private readonly Camera _mainCamera;
        private readonly Rigidbody _rigidbody;
        private readonly System.Func<bool> _isGroundedGetter;
        private readonly System.Func<RaycastHit> _hitGetter;
        private readonly System.Func<Vector3> _overSpeedGetter;
        private readonly System.Func<Vector3> _overNegativeSpeedGetter;
        private readonly System.Func<Vector3> _overPositiveSpeedGetter;
        private readonly System.Func<string> _currentInteractableGetter;
        private readonly System.Func<int> _currentSlotGetter;

        public CharacterDebugInfo(
            Camera mainCamera, 
            Rigidbody rigidbody,
            System.Func<bool> isGroundedGetter,
            System.Func<RaycastHit> hitGetter,
            System.Func<Vector3> overSpeedGetter,
            System.Func<Vector3> overNegativeSpeedGetter,
            System.Func<Vector3> overPositiveSpeedGetter,
            System.Func<string> currentInteractableGetter,
            System.Func<int> currentSlotGetter)
        {
            UnityEngine.Debug.Log("Creating new CharacterDebugInfo instance");
            
            _mainCamera = mainCamera;
            _rigidbody = rigidbody;
            _isGroundedGetter = isGroundedGetter;
            _hitGetter = hitGetter;
            _overSpeedGetter = overSpeedGetter;
            _overNegativeSpeedGetter = overNegativeSpeedGetter;
            _overPositiveSpeedGetter = overPositiveSpeedGetter;
            _currentInteractableGetter = currentInteractableGetter;
            _currentSlotGetter = currentSlotGetter;
            
            RegisterDebugInfo();
        }

        private void RegisterDebugInfo()
        {
            UnityEngine.Debug.Log("Registering debug info");
            var debugManager = DebugDisplayManager.Instance;
            
            // Clear any existing categories first
            debugManager.HideAllCategories();
            
            var inputCategory = debugManager.CreateCategory("Character.Input");
            inputCategory.AddDebugValue("IsGrounded", _isGroundedGetter);
            inputCategory.AddDebugValue("Mouse X", () => Input.GetAxisRaw("Horizontal"));
            inputCategory.AddDebugValue("Mouse Y", () => Input.GetAxisRaw("Vertical"));
            inputCategory.AddDebugValue("Interact Button - E", () => Input.GetKeyDown(KeyCode.E));
            inputCategory.AddDebugValue("Current Slot", _currentSlotGetter);

            var movementCategory = debugManager.CreateCategory("Character.Movement");
            movementCategory.AddDebugValue("Camera Right", () => _mainCamera.transform.right * Input.GetAxisRaw("Horizontal"));
            movementCategory.AddDebugValue("Camera Forward", () => _mainCamera.transform.forward * Input.GetAxisRaw("Vertical"));
            movementCategory.AddDebugValue("Raycast Hit", () => _hitGetter().point);

            var velocityCategory = debugManager.CreateCategory("Character.Velocity");
            velocityCategory.AddDebugValue("Current", () => FormatVector3(_rigidbody.linearVelocity));

            var brakeCategory = debugManager.CreateCategory("Character.BrakeForces");
            brakeCategory.AddDebugValue("Combined", () => FormatVector3(_overSpeedGetter()));
            brakeCategory.AddDebugValue("Negative", () => FormatVector3(_overNegativeSpeedGetter()));
            brakeCategory.AddDebugValue("Positive", () => FormatVector3(_overPositiveSpeedGetter()));

            var interactCategory = debugManager.CreateCategory("Character.Interact");
            interactCategory.AddDebugValue("CurrentInteractItem", _currentInteractableGetter);
        }

        private string FormatVector3(Vector3 vector)
        {
            return $"x: {vector.x:F2} y: {vector.y:F2} z: {vector.z:F2}";
        }
    }
} 