using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States;
using DevFuckers._Project.CodeBase.Runtime.Network;
using Mirror;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameplayScene.GameplayStateMachine.States
{
    public class ExitGameplayState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly CustomNetworkManager _networkManager;

        [Inject]
        public ExitGameplayState(GameStateMachine stateMachine, CustomNetworkManager networkManager)
        {
            _stateMachine = stateMachine;
            _networkManager = networkManager;
        }

        public void Enter()
        {
            // save game
            // clear subscriptions
            // release the addressables assets

            MainStopServer();
            _stateMachine.EnterIn<GameMenuState>();
        }

        public void Exit()
        {
        }

        private void MainStopServer()
        {
            if (NetworkClient.active)
                _networkManager.StopClient();
            
            if (NetworkServer.active)
                NetworkServer.Shutdown();
        
            Debug.Log("stop client/host, Client: " + NetworkClient.active + " Server: "  + NetworkServer.active);
        }
    }
}
