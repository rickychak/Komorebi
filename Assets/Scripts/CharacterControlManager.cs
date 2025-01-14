using Komorebi.Debug;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    private Camera _mainCamera;
    private Rigidbody _rigidbody;
    private DebugDisplayManager _debugDisplayManager;

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
        _debugDisplayManager = DebugDisplayManager.Instance;

        if (_rigidbody == null)
        {
            UnityEngine.Debug.LogError("Cannot find Rigidbody");
        }
        
        if (_mainCamera == null)
        {
            UnityEngine.Debug.LogError("Cannot find Main Camera");
        }
    }

    private void Start()
    {
        RegisterDebugInfo();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RegisterDebugInfo()
    {
        var inputCategory = _debugDisplayManager.CreateCategory("Input");
        inputCategory.AddDebugValue("IsGrounded", () => IsGrounded);
        inputCategory.AddDebugValue("Mouse X", () => Input.GetAxisRaw("Horizontal"));
        inputCategory.AddDebugValue("Mouse Y", () => Input.GetAxisRaw("Vertical"));
        
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
    

    void FixedUpdate()
    {
        if (!IsGrounded) return;
        AddForceOnInput(GetUserInputOnMovement());
        AddBrakeForceOnLimit(_maxSpeed);
    }
    
    
    
}
