using System;
using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.StateMachine
{
    public class GameplayStateMachine : InfrastructureStateMachine
    {
        public event Action StateMachineWasFinished;
        
        public void Finish()
        {
            StateMachineWasFinished?.Invoke();
        }
    }
}
