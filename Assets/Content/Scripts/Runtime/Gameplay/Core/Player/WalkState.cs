using System;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    public class WalkState : StateBase<string>
    {
        // [Inject] private readonly IAnimationService _animationService;
        // [Inject] private readonly ICameraService _cameraService;
        // [Inject] private readonly IAudioService _audioService;
        private IInputService _inputService;
        private PlayerMotionController _motionController;
        private PlayerMotionConfig _motionConfig;
        private Transform _cameraTransform;

        public WalkState(ConfigProvider configProvider, IInputService inputService, ICameraService cameraService, PlayerMotionController motionController, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _motionConfig = configProvider.PayerMotionConfig;
            _inputService = inputService;
            _motionController = motionController;
            _cameraTransform = cameraService.GetMotorCamera().transform;
        }

        public override void OnEnter()
        {
            Debug.Log($"Entering {nameof(WalkState)} state.");

            _inputService.CameraRotateInputChanged += OnCameraRotateInputChanged;

            // set camera bob
            // set walk animation
            // set walk speed
            // set walk sound
        }


        public override void OnExit()
        {
            Debug.Log($"Exiting {nameof(WalkState)} state.");

            _inputService.CameraRotateInputChanged -= OnCameraRotateInputChanged;

            // unset camera bob
            // unset walk animation
            // unset walk speed
            // unset walk sound
        }

        public override void OnLogic()
        {
            base.OnLogic();

            _motionController.Move(_inputService.CurrentMoveInput, _motionConfig.WalkSpeed);
        }

        private void OnCameraRotateInputChanged()
        {
            _motionController.SetRotationDirection(_inputService.CurrentCameraRotateInput, _motionConfig.CameraRotateSpeed, _cameraTransform);
        }
    }
}
