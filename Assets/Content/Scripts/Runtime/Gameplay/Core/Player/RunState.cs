using UnityEngine;
using UnityHFSM;

namespace DevFuckers
{
    internal class RunState : StateBase<string>
    {
        public RunState(bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            Debug.Log($"Entering {nameof(RunState)} state.");
        }
    }
}
