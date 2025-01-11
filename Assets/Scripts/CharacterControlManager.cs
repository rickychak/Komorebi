using System;
using Komorebi.Debug;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    private Camera _mainCamera;
    private Rigidbody _rigidbody;
    private DebugDisplayManager _debugDisplayManager;  
    
    private Vector2 _moveDirection = Vector2.zero;
    
    private bool IsGrounded { get; set; }
    
    private const float _speed = 0.5f;
    private readonly Vector3 _gravity = new(0, -1f, 0);
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _debugDisplayManager = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<DebugDisplayManager>();
    }

    private void Start()
    {
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create("IsGrounded", () => IsGrounded));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" mouse X", () => _moveDirection.x));
        _debugDisplayManager.AppendToDebugObjects(DebugObject.Create(" mouse Y", () => _moveDirection.y));
        // var moveDirectionX = _mainCamera.transform.right * _moveDirection.x;
        // var moveDirectionY = _mainCamera.transform.forward* _moveDirection.y;
        // _debugDisplayManager.AppendToDebugObjects(new DebugObject(" movement X", ref moveDirectionX));
        // _debugDisplayManager.AppendToDebugObjects(new DebugObject(" movement Z", ref moveDirectionY));
        // var rigidbodyLinearVelocity = _rigidbody.linearVelocity;
        // _debugDisplayManager.AppendToDebugObjects(new DebugObject(" velocity X", ref rigidbodyLinearVelocity.x));
        // _debugDisplayManager.AppendToDebugObjects(new DebugObject(" velocity Y", ref rigidbodyLinearVelocity.y));
        // _debugDisplayManager.AppendToDebugObjects(new DebugObject(" velocity Z", ref rigidbodyLinearVelocity.z));
    }

    void GetUserInputOnMovement()
    {
        _moveDirection.x = Math.Abs(_rigidbody.linearVelocity.x) < 2.5f? Input.GetAxisRaw("Horizontal"): 0;
        _moveDirection.y = Math.Abs(_rigidbody.linearVelocity.z) < 2.5f? Input.GetAxisRaw("Vertical"): 0;
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

    void FixedUpdate()
    {
        
        if (!IsGrounded) return;
        GetUserInputOnMovement();
        Vector3 move = _mainCamera.transform.forward* _moveDirection.y + _mainCamera.transform.right * _moveDirection.x;
        move.y = 0;
        _rigidbody.AddForce(move * _speed, ForceMode.VelocityChange);
    }
    
    
    
}
