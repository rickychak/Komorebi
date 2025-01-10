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
    
    private bool _isGrounded = false;
    
    private const float _speed = 0.5f;
    private readonly Vector3 _gravity = new(0, -1f, 0);
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _debugDisplayManager = GameObject.FindGameObjectWithTag("EditorOnly").GetComponentInChildren<DebugDisplayManager>();
    }

    private void Start()
    {
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("isGrounded", _isGrounded));
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("mouse X", _moveDirection.x));
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("mouse y", _moveDirection.y));
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("movement X", _mainCamera.transform.right * _moveDirection.x));
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("movement Z", _mainCamera.transform.forward* _moveDirection.y));
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("velocity X", _rigidbody.linearVelocity.x));
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("velocity Y", _rigidbody.linearVelocity.y));
        _debugDisplayManager.AppendToDebugObjects(new DebugObject("velocity Z", _rigidbody.linearVelocity.z));
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
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        
        if (!_isGrounded) return;
        GetUserInputOnMovement();
        Vector3 move = _mainCamera.transform.forward* _moveDirection.y + _mainCamera.transform.right * _moveDirection.x;
        move.y = 0;
        _rigidbody.AddForce(move * _speed, ForceMode.VelocityChange);
    }
    
    
    
}
