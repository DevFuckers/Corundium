using System;
using System.Threading.Tasks;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.ActionStateMachine
{
    public class SwingState : StateBase<string>
    {
        public event Action OnSwingEndEvent;
        private readonly PlayerAnimationController _playerAnimationController;
        private readonly PlayerMotionConfig _motionConfig;

        public SwingState(PlayerAnimationController playerAnimationController, ConfigProvider configProvider, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _playerAnimationController = playerAnimationController;
            _motionConfig = configProvider.PayerMotionConfig;
        }

        public override async void OnEnter()
        {
            Debug.Log($"Entering {nameof(SwingState)} state.");

            _playerAnimationController.SetAnimatorLayerWeight(1, 1f);
            _playerAnimationController.SetAnimationState("Attack_2", layerIndex: 1);

            await Task.Delay(500);

            OnSwingEndEvent?.Invoke();
        }
    }
}
