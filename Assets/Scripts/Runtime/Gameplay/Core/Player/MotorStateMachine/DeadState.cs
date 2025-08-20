using UnityEngine;
using UnityHFSM;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.MotorStateMachine
{
    public class DeadState : StateBase<string>
    {
        public DeadState(bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
        }
    }
}
