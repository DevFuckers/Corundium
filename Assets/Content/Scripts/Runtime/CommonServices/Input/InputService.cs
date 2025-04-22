using System;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input
{
    public class InputService : IInputService
    {
        public event Action MoveInputChanged = delegate { };
        public Vector2 CurrentMoveInput { get; private set; }

        public event Action CameraRotateInputChanged = delegate { };
        public Vector2 CurrentCameraRotateInput { get; private set; }

        private PlayerInput _input;
        private PlayerInput Input => _input ??= new PlayerInput();

        public void Enable()
        {
            Input.Enable();
            Input.Player.Move.performed += OnMoveInputChanged;
            Input.Player.Move.canceled += OnMoveInputChanged;
            Input.Player.Look.performed += OnCameraRotateInputChanged;
        }

        public void Disable()
        {
            Input.Disable();
            Input.Player.Move.performed -= OnMoveInputChanged;
            Input.Player.Move.canceled -= OnMoveInputChanged;
            Input.Player.Look.performed -= OnCameraRotateInputChanged;
        }

        private void OnMoveInputChanged(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            CurrentMoveInput = context.ReadValue<Vector2>();
            MoveInputChanged?.Invoke();
        }

        public void OnCameraRotateInputChanged(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            CurrentCameraRotateInput = context.ReadValue<Vector2>();
            CameraRotateInputChanged?.Invoke();
        }
    }
}
