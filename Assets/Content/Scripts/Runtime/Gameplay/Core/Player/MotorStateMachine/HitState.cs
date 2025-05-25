using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.MotorStateMachine
{
    public class HitState : StateBase<string>
    {
        private readonly PlayerMotionConfig _motionConfig;
        private readonly PlayerMotionController _motionController;
        private readonly PlayerAnimationController _animationController;
        private readonly Transform _rootCameraTransform;
        private readonly object _startPos;

        public HitState(ConfigProvider configProvider, ICameraService cameraService, 
            PlayerMotionController motionController, PlayerAnimationController playerAnimationController, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            // _motionConfig = configProvider.PayerMotionConfig;
            _motionController = motionController;
            _animationController = playerAnimationController;
            _rootCameraTransform = cameraService.GetRootCamera().transform;
            _startPos = _rootCameraTransform.localPosition;
        }
        public override void OnEnter()
        {
            Debug.Log($"Entering {nameof(HitState)} state.");

            _animationController.SetAnimationState("Hit", layerIndex: 0, transitionTime: 0.1f);
        }
    }
}
