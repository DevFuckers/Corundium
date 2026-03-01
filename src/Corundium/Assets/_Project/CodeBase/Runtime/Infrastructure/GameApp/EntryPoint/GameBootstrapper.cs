using DevFuckers._Project.CodeBase.Runtime.Common.Factories.StateFactory;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.EntryPoint
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine.GameStateMachine _gameStateMachine;
        private StateFactory _stateFactory;

        [Inject]
        public void Construct(GameStateMachine.GameStateMachine gameStateMachine, StateFactory stateFactory)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
        }

        private void Start()
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<GameBootstrapState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GameMenuState>());
            _gameStateMachine.RegisterState(_stateFactory.Create<GamePlayLoopState>());

            _gameStateMachine.EnterIn<GameBootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
