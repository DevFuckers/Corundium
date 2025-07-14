using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.StateFactory;
using DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.EntryPoint
{
    public class GameStateMachineInit : MonoBehaviour
    {
        private StateFactory _stateFactory;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(StateFactory stateFactory, GameStateMachine gameStateMachine)
        {
            _stateFactory = stateFactory;
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            _gameStateMachine.RegisterState(_stateFactory.CreateState<GameInitState>());
            _gameStateMachine.RegisterState(_stateFactory.CreateState<GameMenuState>());
            _gameStateMachine.RegisterState(_stateFactory.CreateState<GameplayState>());
        
            _gameStateMachine.EnterIn<GameInitState>();

            DontDestroyOnLoad(this);
        }
    }
}
