using System;
using Komorebi.Debug;
using Komorebi.Product;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    private Camera _mainCamera;
    private Rigidbody _rigidbody;
    private DebugDisplayManager _debugDisplayManager;
    private CapsuleCollider _capsuleCollider;
    
    private IInteractable _currentInteractable;
    private float _interactionCheckCooldown = 0.1f; // Adjust as needed
    private float _nextInteractionCheck;

    private Vector3 _overPositiveSpeed;
    private Vector3 _overNegativeSpeed;
    private Vector3 _overSpeed;
    
    private const float _speed = 0.5f;
    private const float _maxSpeed = 10f;
    
    private bool IsGrounded { get; set; }
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _debugDisplayManager = DebugDisplayManager.Instance;

        if (_rigidbody == null){
            UnityEngine.Debug.LogError("Cannot find Rigidbody");
        }
        if (_mainCamera == null){
            UnityEngine.Debug.LogError("Cannot find Main Camera");
        }
    }

    private void Start()
    {
        RegisterDebugInfo();
        _rigidbody.linearDamping = 0f;
        _rigidbody.angularDamping = 0f;
    }

    private void RegisterDebugInfo()
    {
        var inputCategory = _debugDisplayManager.CreateCategory("Input");
        inputCategory.AddDebugValue("IsGrounded", () => IsGrounded);
        inputCategory.AddDebugValue("Mouse X", () => Input.GetAxisRaw("Horizontal"));
        inputCategory.AddDebugValue("Mouse Y", () => Input.GetAxisRaw("Vertical"));
        inputCategory.AddDebugValue("Interact Button - E", () => Input.GetKeyDown(KeyCode.E));
        
        var movementCategory = _debugDisplayManager.CreateCategory("Movement");
        movementCategory.AddDebugValue("Camera Right", () => _mainCamera.transform.right * Input.GetAxisRaw("Horizontal"));
        movementCategory.AddDebugValue("Camera Forward", () => _mainCamera.transform.forward * Input.GetAxisRaw("Vertical"));
        
        var velocityCategory = _debugDisplayManager.CreateCategory("Velocity");
        velocityCategory.AddDebugValue("Current", () => $"x: {_rigidbody.linearVelocity.x:F2} " +
                                                       $"y: {_rigidbody.linearVelocity.y:F2} " +
                                                       $"z: {_rigidbody.linearVelocity.z:F2}");
        
        var brakeCategory = _debugDisplayManager.CreateCategory("Brake Forces");
        brakeCategory.AddDebugValue("Combined", () => FormatVector3(_overSpeed));
        brakeCategory.AddDebugValue("Negative", () => FormatVector3(_overNegativeSpeed));
        brakeCategory.AddDebugValue("Positive", () => FormatVector3(_overPositiveSpeed));
        
        var interactCategory = _debugDisplayManager.CreateCategory("Interact");
        interactCategory.AddDebugValue("CurrentInteractItem", RetrieveCurrentInteractableItem);
    }

    private string FormatVector3(Vector3 vector)
    {
        return $"x: {vector.x:F2} y: {vector.y:F2} z: {vector.z:F2}";
    }

    private Vector2 GetUserInputOnMovement()
    {
        Vector2 moveDirection = Vector2.zero;
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        return moveDirection;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InteractableItem"))
        {
            other.gameObject.GetComponent<IInteractable>().ShowPrompt();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractableItem"))
        {
            other.gameObject.GetComponent<IInteractable>().ShowPrompt();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

    private void AddForceOnInput(Vector2 axisInput)
    {
        Vector3 move = _mainCamera.transform.forward * axisInput.y + _mainCamera.transform.right * axisInput.x;
        move.y = 0;
        move = move.normalized * _speed;
        _rigidbody.linearVelocity = move * _maxSpeed;
    }

    private void AddBrakeForceOnLimit(float maxSpeed)
    {
        if (_rigidbody.linearVelocity.magnitude > maxSpeed)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * maxSpeed;
        }
    }

    private void Update()
    {
        if (Time.time >= _nextInteractionCheck)
        {
            UpdateInteractableDetection();
            _nextInteractionCheck = Time.time + _interactionCheckCooldown;
        }
        
        // Only perform interaction when E is pressed
        if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }

    void FixedUpdate()
    {
        if (!IsGrounded) return;
        AddForceOnInput(GetUserInputOnMovement());
        AddBrakeForceOnLimit(_maxSpeed);
    }

    private string RetrieveCurrentInteractableItem()
    {
        string currentInteractableItem = "null";
        if (_currentInteractable != null)
        {
            currentInteractableItem = _currentInteractable.GetGameObject().gameObject.name;
        }

        return currentInteractableItem;
    }

    void UpdateInteractableDetection()
    {
        RaycastHit hit;
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * (_capsuleCollider.radius * 2), out hit))
        {
            // First check if it's an interactable layer/tag
            if (hit.collider.CompareTag("InteractableItem")) // Make sure to set this tag on interactable objects
            {
                // Only get component if we don't already have it cached
                if (_currentInteractable == null || hit.collider.gameObject != _currentInteractable.GetGameObject())
                {
                    _currentInteractable = hit.collider.gameObject.GetComponent<IInteractable>();
                }
            }
            else
            {
                _currentInteractable = null;
            }
        }
        else
        {
            _currentInteractable = null;
        }
    }
    
    
    
    
}
