using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    private bool CanMove { get; set; } = true;

    private InputManager _inputManager;

    private Rigidbody _rb;
    private float _vertSpeed;
    public float minFall = -1.5f;
    private bool isGrounded;

    private Vector2 _movementInput;
    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private bool _isSprinting = false;
    
    private Transform playerCamera;
    public Transform playerTransform;
    private Vector2 _currentInput;



    void Start() {
        _inputManager = InputManager.Instance;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = Camera.main.transform;

    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _vertSpeed = minFall;
    }

    void Update() {
        
        if (CanMove) 
            ProcessMovement();
        
        // if (Input.GetMouseButtonDown(0)) {
        //     RaycastHit hit;
        //     if
        //         (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,
        //             100)) {
        //         agent.destination = hit.point;
        //     }
        // }
    }
    
    private void ProcessMovement()
    {
        float speed = GetMovementSpeed();
        CalculateMovementVector();
        ApplyMovementToController();
        // RotatePlayerWithCamera();
    }
    
    private void CalculateMovementVector()
    {
        Vector2 movement = _inputManager.GetMovementVector();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = playerCamera.forward * move.z + playerCamera.right * move.x;
        move.y = 0f;
        Vector3 right = _rb.transform.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up);
        
        _moveDirection = (right * move.x) + (forward * move.y);
        _moveDirection *= GetMovementSpeed();
        _moveDirection = Vector3.ClampMagnitude(_moveDirection, GetMovementSpeed());

        // float horInput = Input.GetAxis("Horizontal");
        // float vertInput = Input.GetAxis("Vertical");
        // Vector3 right = _rb.transform.right;
        // Vector3 forward = Vector3.Cross(right, Vector3.up);
        //
        // _moveDirection = (right * horInput) + (forward * vertInput);
        // _moveDirection *= GetMovementSpeed();
        // _moveDirection = Vector3.ClampMagnitude(_moveDirection, GetMovementSpeed());
    }
    
    
    private void ApplyMovementToController()
    {
        _moveDirection.y = _vertSpeed;
        float speed = GetMovementSpeed();
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed;

        _rb.velocity = new Vector3(_moveDirection.x, _rb.velocity.y, _moveDirection.z);
    }
    
    private void RotatePlayerWithCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        // transform.rotation *= Quaternion.Euler(0, mouseX * lookSpeedX, 0);
        transform.rotation = Quaternion.Euler(playerCamera.eulerAngles.x, playerCamera.eulerAngles.y, 0);
    }

    
    private float GetMovementSpeed()
    {
        _isSprinting = Input.GetButton("Fire3");
        return Input.GetButton("Fire3") ? runSpeed : walkSpeed;
    }

}
