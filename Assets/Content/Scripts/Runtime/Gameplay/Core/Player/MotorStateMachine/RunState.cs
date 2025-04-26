using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.MotorStateMachine
{
    public class RunState : StateBase<string>
    {
        private readonly IInputService _inputService;
        private readonly PlayerMotionController _motionController;
        private readonly PlayerAnimationController _animationController;
        private readonly PlayerMotionConfig _motionConfig;
        private readonly Transform _rootCameraTransform;
        private readonly Transform _walkCameraTransform;

        private float _bobTimerX = 0f;
        private float _bobTimerY = 0f;
        private Vector3 _startPos;

        public RunState(ConfigProvider configProvider, IInputService inputService, ICameraService cameraService, PlayerMotionController motionController, PlayerAnimationController playerAnimationController, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _motionConfig = configProvider.PayerMotionConfig;
            _inputService = inputService;
            _motionController = motionController;
            _animationController = playerAnimationController;
            _rootCameraTransform = cameraService.GetRootCamera().transform;
            _walkCameraTransform = cameraService.GetWalkCamera().transform;
            _startPos = _walkCameraTransform.localPosition;
        }

        public override void OnEnter()
        {
            Debug.Log($"Entering {nameof(RunState)} state.");

            _inputService.CameraRotateInputChanged += OnCameraRotateInputChanged;

            _motionController.SetBobDesiredPosition(_startPos);
            _animationController.SetAnimationState("Run", layerIndex: 0);
        }


        public override void OnExit()
        {
            Debug.Log($"Exiting {nameof(RunState)} state.");

            _inputService.CameraRotateInputChanged -= OnCameraRotateInputChanged;
        
            _bobTimerY = 0f;
            _bobTimerX = 0f;
            _motionController.SetBobDesiredPosition(_startPos);
        }

        public override void OnLogic()
        {
            base.OnLogic();

            _motionController.UpdateGravity(_motionConfig.Gravity);
            _motionController.Move(_inputService.CurrentMoveInput, _motionConfig.RunSpeed, _motionConfig.SmoothRunDeltaTime);

            AddBob();

            _walkCameraTransform.localPosition = _motionController.UpdateBob(_motionConfig.BobbingFadeSpeed, _motionConfig.BobbingSetDestinationSpeed);
        }

        private void OnCameraRotateInputChanged()
        {
            _motionController.SetRotationDirection(_inputService.CurrentCameraRotateInput, _motionConfig.CameraRotateSpeed, _rootCameraTransform);
        }

        private void AddBob()
        {
            _bobTimerX += Time.deltaTime * _motionConfig.RunBobbingFrequencyX;
            _bobTimerY += Time.deltaTime * _motionConfig.RunBobbingFrequencyY;

            float bobX = Mathf.Cos(_bobTimerX * 2) * _motionConfig.RunBobbingHorizontalAmplitude;
            float bobY = Mathf.Sin(_bobTimerY) * _motionConfig.RunBobbingVerticalAmplitude;

            _motionController.SetBobDesiredPosition(new Vector2(bobX, bobY));
        }
    }
}
