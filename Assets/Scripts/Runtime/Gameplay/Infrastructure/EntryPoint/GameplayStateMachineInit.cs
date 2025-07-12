using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.StateFactory;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.EntryPoint
{
    public class GameplayStateMachineInit : MonoBehaviour
    {
        private StateFactory _stateFactory;
        private GameplayStateMachine _gameplayStateMachine;

        [Inject]
        public void Construct(StateFactory stateFactory, GameplayStateMachine gameplayStateMachine)
        {
            _stateFactory = stateFactory;
            _gameplayStateMachine = gameplayStateMachine;
        }

        public GameplayStateMachine Init()
        {
            _gameplayStateMachine.RegisterState(_stateFactory.CreateState<GameplayInitState>());

            _gameplayStateMachine.EnterIn<GameplayInitState>();

            return _gameplayStateMachine;
        }
    }
}
