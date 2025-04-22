using System;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input
{
    public interface IInputService
    {
        public event Action MoveInputChanged;
        public Vector2 CurrentMoveInput { get; }

        public event Action CameraRotateInputChanged;
        public Vector2 CurrentCameraRotateInput { get; }

        void Enable();
        void Disable();
    }
}
