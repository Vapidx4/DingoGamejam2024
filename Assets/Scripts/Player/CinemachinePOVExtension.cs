using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CinemachinePOVExtension : CinemachineExtension
    {
        private InputManager _inputManager;
        private Vector3 startingRotation;
        public Transform playerTransform;
        
        [SerializeField] private float hSpeed = 10f;
        [SerializeField] private float vSpeed = 10f;
        [SerializeField] private float clampAngle = 80f;

        protected override void Awake()
        {
            _inputManager = InputManager.Instance;
            // playerTransform = transform;
            base.Awake();
        }
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage,
            ref CameraState state, float deltaTime)
        {
            if (vcam.Follow) {
                if (stage == CinemachineCore.Stage.Aim) {
                    if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                    Vector2 deltaInput = _inputManager.GetMouseDelta();
                    startingRotation.x += deltaInput.x * vSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * hSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    playerTransform.localRotation = Quaternion.Euler(0f, startingRotation.x, 0f);

                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
            }
        }
    }
    
    
}

