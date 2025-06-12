using System;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine
{
    public class SceneInsfrastructureStateMachine : InfrastructureStateMachine
    {
        public event Action StateMachineWasFinished;

        public void Finish()
        {
            StateMachineWasFinished?.Invoke();
        }
    }
}
