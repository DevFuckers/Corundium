using System;
using System.Threading.Tasks;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.SO;
using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.ActionStateMachine
{
    public class AttackState : StateBase<string>
    {
        public event Action OnAttackEvent;

        public AttackState(PlayerAnimationController playerAnimationController, ConfigProvider configProvider, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
        }

        public override async void OnEnter()
        {
            // base.OnLogic();

            // _playerAnimationController.SetAnimatorLayerWeight(1, 1f);
            // _playerAnimationController.SetAnimationState("Attack_2", layerIndex: 1);

            Debug.Log($"Entering {nameof(AttackState)} state.");

            await Task.Delay(500);

            OnAttackEvent.Invoke();
        }
    }
}
