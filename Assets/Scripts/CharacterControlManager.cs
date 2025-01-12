using System;
using Komorebi.Debug;
using UnityEngine.InputSystem;
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
        _debugDisplayManager = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<DebugDisplayManager>();
    }

    private void Start()
    {
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" IsGrounded", () => IsGrounded));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" mouse X", () => Input.GetAxisRaw("Horizontal")));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" mouse Y", () => Input.GetAxisRaw("Vertical")));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" movement X", () => _mainCamera.transform.right*Input.GetAxisRaw("Horizontal")));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" movement Z", () => _mainCamera.transform.forward * Input.GetAxisRaw("Vertical")));
        var velocity = _mainCamera.transform.forward* GetUserInputOnMovement().y + _mainCamera.transform.right * GetUserInputOnMovement().x;
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" movement summary", () => "x: " + velocity.x + " y: " + velocity.y + " z: " + velocity.z));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" velocity X", () => _rigidbody.linearVelocity.x));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" velocity Y", () => _rigidbody.linearVelocity.y));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" velocity Z", () => _rigidbody.linearVelocity.z));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" brakeForce", () => " x: " + _overSpeed.x + " y: " + _overSpeed.y + " z: " + _overSpeed.z));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" brakeForce -ve", () => " x: " + _overNegativeSpeed.x + " y: " + _overNegativeSpeed.y + " z: " + _overNegativeSpeed.z));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" brakeForce +ve", () => " x: " + _overPositiveSpeed.x + " y: " + _overPositiveSpeed.y + " z: " + _overPositiveSpeed.z));

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
        Vector3 move = _mainCamera.transform.forward* axisInput.y + _mainCamera.transform.right * axisInput.x;
        move.y = 0;
        _rigidbody.AddForce(move * _speed, ForceMode.VelocityChange);
    }

    private void AddBrakeForceOnLimit(float maxSpeed)
    {
        _overPositiveSpeed = new Vector3((_rigidbody.linearVelocity.x > 0 && _rigidbody.linearVelocity.x > maxSpeed)? maxSpeed - _rigidbody.linearVelocity.x: 0, (_rigidbody.linearVelocity.y > 0 && _rigidbody.linearVelocity.y > maxSpeed)? maxSpeed - _rigidbody.linearVelocity.y: 0, (_rigidbody.linearVelocity.z > 0 && _rigidbody.linearVelocity.z > maxSpeed)? maxSpeed - _rigidbody.linearVelocity.z: 0);
        _overNegativeSpeed = new Vector3((_rigidbody.linearVelocity.x < 0 && _rigidbody.linearVelocity.x < -1 * maxSpeed)? -1 * maxSpeed - _rigidbody.linearVelocity.x: 0, (_rigidbody.linearVelocity.y < 0 && _rigidbody.linearVelocity.y < -1 * maxSpeed)? -1 * maxSpeed - _rigidbody.linearVelocity.y: 0, (_rigidbody.linearVelocity.z < 0 && _rigidbody.linearVelocity.z < -1 * maxSpeed)? -1 * maxSpeed - _rigidbody.linearVelocity.z: 0);
        _overSpeed = _overPositiveSpeed + _overNegativeSpeed;
        _rigidbody.AddForce( -1 * _overSpeed, ForceMode.VelocityChange);
    }
    

    void FixedUpdate()
    {
        if (!IsGrounded) return;
        AddForceOnInput(GetUserInputOnMovement());
        AddBrakeForceOnLimit(_maxSpeed);
    }
    
    
    
}
