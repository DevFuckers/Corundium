using System;
using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.StateMachine
{
    public class MainMenuStateMachine : InfrastructureStateMachine
    {
        public event Action StateMachineWasFinishedWithHostStarted;

        public void FinishWithStartHost()
        {
            StateMachineWasFinishedWithHostStarted?.Invoke();
        }
    }
}
