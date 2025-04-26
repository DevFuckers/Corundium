using System;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input
{
    public interface IInputService
    {
        public event Action AttackInputPressed;
        public event Action MoveInputChanged;
        public event Action CameraRotateInputChanged;
        
        public Vector2 CurrentMoveInput { get; }
        public Vector2 CurrentCameraRotateInput { get; }

        bool JumpState { get; }
        bool RunState { get; }


        void Enable();
        void Disable();
    }
}
