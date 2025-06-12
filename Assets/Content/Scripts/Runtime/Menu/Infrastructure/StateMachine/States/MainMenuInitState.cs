using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Menu.Core;
using DevFuckers.Assets.Content.Scripts.Runtime.Network;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.StateMachine.States
{
    public class MainMenuInitState : IState
    {
        [Inject] private readonly CustomNetworkManager _customNetworkManager;
        [Inject] private readonly SetUpMenuUI _setUpMenuUI;
        [Inject] private readonly MainMenuStateMachine _mainMenuStateMachine;

        public void Enter()
        {
            _customNetworkManager.Init();
            _setUpMenuUI.Init();

            _customNetworkManager.OnServerStarted += FinishWithStartHost;
        }

        public void Exit()
        {
            _customNetworkManager.OnServerStarted -= FinishWithStartHost;
        }

        private void FinishWithStartHost()
        {
            _mainMenuStateMachine.FinishWithStartHost();
        }
    }
}
