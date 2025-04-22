using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    internal class IdleState : StateBase<string>
    {
        private IInputService _inputService;
        private PlayerMotionController _motionController;
        private PlayerMotionConfig _motionConfig;
        private Transform _cameraTransform;
        
        public IdleState(ConfigProvider configProvider, IInputService inputService, ICameraService cameraService, PlayerMotionController motionController, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _motionConfig = configProvider.PayerMotionConfig;
            _inputService = inputService;
            _motionController = motionController;
            _cameraTransform = cameraService.GetMotorCamera().transform;
        }

        public override void OnEnter()
        {
            Debug.Log($"Entering {nameof(IdleState)} state.");

            _inputService.CameraRotateInputChanged += OnCameraRotateInputChanged;
        }

        public override void OnExit()
        {
            Debug.Log($"Exiting {nameof(IdleState)} state.");

            _inputService.CameraRotateInputChanged -= OnCameraRotateInputChanged;
        }
    
        private void OnCameraRotateInputChanged()
        {
            _motionController.SetRotationDirection(_inputService.CurrentCameraRotateInput, _motionConfig.CameraRotateSpeed, _cameraTransform);
        }

    }   
}
