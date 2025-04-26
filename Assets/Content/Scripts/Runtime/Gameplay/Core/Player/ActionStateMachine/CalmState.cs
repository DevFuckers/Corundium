using System.Threading.Tasks;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.ActionStateMachine
{
    public class CalmState : StateBase<string>
    {
        private readonly PlayerAnimationController _playerAnimationController;
        private readonly PlayerMotionConfig _motionConfig;

        public CalmState(PlayerAnimationController playerAnimationController, ConfigProvider configProvider, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _playerAnimationController = playerAnimationController;
            _motionConfig = configProvider.PayerMotionConfig;
        }

        public override async void OnEnter()
        {
            Debug.Log($"Entering {nameof(CalmState)} state.");

            _playerAnimationController.SetAnimationState("Calm", layerIndex: 1, transitionTime: 0.25f);

            await Task.Delay(300);
            _playerAnimationController.SetAnimatorLayerWeight(1, 0);
        }
    }
}
