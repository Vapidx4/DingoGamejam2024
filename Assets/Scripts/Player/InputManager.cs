using System;
using UnityEngine;


    public class InputManager : MonoBehaviour
    {
        public static InputManager _instance;

        public static InputManager Instance
        {
            get
            {
                return _instance;
            }
        }
        
        private PlayerControls _playerControls;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        public Vector2 GetMovementVector()
        {
            return _playerControls.Player.Movement.ReadValue<Vector2>();
        }
        
        public Vector2 GetMouseDelta()
        {
            return _playerControls.Player.Look.ReadValue<Vector2>();
        }

        public bool PlayerJumpedThisFrame()
        {
            return _playerControls.Player.Jump.triggered;
        }
        
        public bool FireButtonPressed()
        {
            // Debug.Log(_playerControls.Player.Fire.triggered);
            return _playerControls.Player.Fire.triggered;
            // return _playerControls.Player.Fire.ReadValue<float>() > 0.0f;

        }
        
        public bool FireButtonHeld()
        {
            // Debug.Log(_playerControls.Player.Fire.triggered);
            // return _playerControls.Player.Fire.triggered;
            return _playerControls.Player.Fire.ReadValue<float>() > 0.0f;

        }
    }