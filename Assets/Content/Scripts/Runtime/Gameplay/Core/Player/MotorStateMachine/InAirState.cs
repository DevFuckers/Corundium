using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.MotorStateMachine
{
    public class InAirState : StateBase<string>
    {
        private readonly IInputService _inputService;
        private readonly PlayerMotionController _motionController;
        private readonly PlayerMotionConfig _motionConfig;
        private readonly Transform _walkCameraTransform;

        public InAirState(ConfigProvider configProvider, IInputService inputService, ICameraService cameraService,
            PlayerMotionController motionController, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _motionConfig = configProvider.PayerMotionConfig;
            _inputService = inputService;
            _motionController = motionController;
            _walkCameraTransform = cameraService.GetWalkCamera().transform;
        }

        public override void OnEnter()
        {
            Debug.Log($"Entering {nameof(InAirState)} state.");
        }

        public override void OnLogic()
        {
            base.OnLogic();

            _motionController.UpdateGravity(_motionConfig.Gravity);
            _motionController.Move(_inputService.CurrentMoveInput, _motionConfig.WalkSpeed, _motionConfig.SmoothWalkDeltaTime);

            _walkCameraTransform.localPosition = _motionController.UpdateBob(_motionConfig.BobbingFadeSpeed, _motionConfig.BobbingSetDestinationSpeed);
        }
    }
}
