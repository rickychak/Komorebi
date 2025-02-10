using System;
// using Komorebi.GameFlow;
using Komorebi.Debug;
using Komorebi.Product;
using Product;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    private const float _speed = 0.5f;
    private const float _maxSpeed = 10f;
    
    private float _interactionCheckCooldown = 0.1f; // Adjust as needed
    private float _nextInteractionCheck;
    
    private InteractableItem _currentInteractable;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;
    private InventoryController _inventoryController;
    
    private Vector3 _overPositiveSpeed;
    private Vector3 _overNegativeSpeed;
    private Vector3 _overSpeed;
    
    private RaycastHit _hit;
    
    [SerializeField]
    private LayerMask _layerMask;
    
    
    
    private bool IsGrounded { get; set; }
    private CharacterDebugInfo _debugInfo;

    void Awake()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _inventoryController = new InventoryController();

        if (_rigidbody == null){
            UnityEngine.Debug.LogError("Cannot find Rigidbody");
        }
        if (_mainCamera == null){
            UnityEngine.Debug.LogError("Cannot find Main Camera");
        }

        // Initialize debug info
        _debugInfo = new CharacterDebugInfo(
            _mainCamera,
            _rigidbody,
            () => IsGrounded,
            () => _hit,
            () => _overSpeed,
            () => _overNegativeSpeed,
            () => _overPositiveSpeed,
            RetrieveCurrentInteractableItem,
            () => _inventoryController.CurrentSlot
        );
    }

    private void Start()
    {
        _rigidbody.linearDamping = 0f;
        _rigidbody.angularDamping = 0f;
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
            UpdateCameraRaycastDetection();
            _nextInteractionCheck = Time.time + _interactionCheckCooldown;
        }

        HandleNumpadInput();
        HandleInputOnInteractables();
        
        if (Input.GetKeyDown(KeyCode.Q) && _hit.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            var slotGameObject = _inventoryController.RemoveItemFromInventory();
            if (slotGameObject)
            {
                slotGameObject.transform.position = _hit.point;
            }
        }
    }

    private void HandleNumpadInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_inventoryController.CurrentSlot == 4)
            {
                _inventoryController.CurrentSlot = 0;
                return;
            }
            _inventoryController.CurrentSlot += 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_inventoryController.CurrentSlot == 0)
            {
                _inventoryController.CurrentSlot = 4;
                return;
            }
            _inventoryController.CurrentSlot -= 1;
        }
    }
    
    private void HandleInputOnInteractables()
    {
        if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null)
        {
            if (_currentInteractable is IEquipmentInteraction equipment)
            {
                equipment.OnInteract();
            }
            else
            {
                _currentInteractable.TriggerAnimation();
            }
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
            currentInteractableItem = _currentInteractable.GetGameObject().name;
        }

        return currentInteractableItem;
    }

    void UpdateCameraRaycastDetection()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, (_capsuleCollider.radius * 5)))
        {
            _hit = hit;
            if (hit.collider.CompareTag("InteractableItem"))
            {
                if (_currentInteractable == null || hit.collider.gameObject != _currentInteractable.GetGameObject())
                {
                    _currentInteractable = hit.collider.gameObject.GetComponentInParent<InteractableItem>();
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
