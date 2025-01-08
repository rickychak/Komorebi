using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    private CharacterController _characterController;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;
    
    private Vector2 _moveDirection = Vector2.zero;
    private Vector3 _movement = Vector3.zero;
    
    private bool _isGrounded = false;
    
    private const float _speed = 0.5f;
    private readonly Vector3 _gravity = new(0, -1f, 0);
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }

    void GetUserInputOnMovement()
    {
        _moveDirection.x = Input.GetAxisRaw("Horizontal");
        _moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    void ImposeMovementOnInput()
    {
        _movement.x = _moveDirection.x;
        _movement.z = _moveDirection.y;
        _characterController.Move(_movement * (Time.deltaTime * _speed));
    }

    void ImposeCameraRotationOnInput()
    {
        Vector3 currentLookDirection = _mainCamera.transform.forward;
        currentLookDirection.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(currentLookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _speed * Time.deltaTime);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void ProcessMovementOnInput()
    {
        if (!_characterController.isGrounded) return;
        GetUserInputOnMovement();
        ImposeMovementOnInput();
        ImposeCameraRotationOnInput();
    }

    void ApplyGravity()
    {
        if (!_characterController.isGrounded && _characterController.velocity.y > -25f)
        {
            _characterController.Move(_gravity * (10f * Time.deltaTime));
        }
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
        if (Math.Abs(_rigidbody.linearVelocity.x) > 5f ||Math.Abs( _rigidbody.linearVelocity.y) > 5f || Math.Abs(_rigidbody.linearVelocity.z) > 5f) return;
        _rigidbody.AddForce(move * _speed, ForceMode.VelocityChange);
    }
    
    
    
}
