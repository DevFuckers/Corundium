using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.MotorStateMachine
{
    public class JumpState : StateBase<string>
    {
        private readonly IInputService _inputService;
        private readonly PlayerMotionController _motionController;
        private readonly PlayerAnimationController _animationController;
        private readonly PlayerMotionConfig _motionConfig;
        private readonly Transform _cameraTransform;

        public JumpState(ConfigProvider configProvider, IInputService inputService, ICameraService cameraService, 
            PlayerMotionController motionController, PlayerAnimationController playerAnimationController, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _motionConfig = configProvider.PayerMotionConfig;
            _inputService = inputService;
            _motionController = motionController;
            _animationController = playerAnimationController;
            _cameraTransform = cameraService.GetRootCamera().transform;
        }

        public override void OnEnter()
        {
            Debug.Log($"Entering {nameof(JumpState)} state.");

            _inputService.CameraRotateInputChanged += OnCameraRotateInputChanged;

            _motionController.Jump(_motionConfig.JumpForce);
            _animationController.SetAnimationState("Jump", layerIndex: 0);
        }


        public override void OnExit()
        {
            Debug.Log($"Exiting {nameof(JumpState)} state.");

            _inputService.CameraRotateInputChanged -= OnCameraRotateInputChanged;

        }

        public override void OnLogic()
        {
            base.OnLogic();

            _motionController.UpdateGravity(_motionConfig.Gravity);
            _motionController.Move(_inputService.CurrentMoveInput, _motionConfig.WalkSpeed);
        }

        private void OnCameraRotateInputChanged()
        {
            _motionController.SetRotationDirection(_inputService.CurrentCameraRotateInput, _motionConfig.CameraRotateSpeed, _cameraTransform);
        }
    }
}
