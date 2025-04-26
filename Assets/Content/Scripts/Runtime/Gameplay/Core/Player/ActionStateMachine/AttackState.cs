using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.ActionStateMachine
{
    public class AttackState : StateBase<string>
    {
        private readonly PlayerAnimationController _playerAnimationController;
        private readonly PlayerMotionConfig _motionConfig;

        public AttackState(PlayerAnimationController playerAnimationController, ConfigProvider configProvider, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _playerAnimationController = playerAnimationController;
            _motionConfig = configProvider.PayerMotionConfig;
        }

        public override void OnEnter()
        {
            // base.OnLogic();

            _playerAnimationController.SetAnimatorLayerWeight(1, 1f);
            _playerAnimationController.SetAnimationState("Attack_2", layerIndex: 1);
        
            Debug.Log($"Entering {nameof(AttackState)} state.");
        }
    }
}
