using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.StateFactory;
using DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.EntryPoint
{
    public class MainMenuStateMachineInit : MonoBehaviour
    {
        private StateFactory _stateFactory;
        private MainMenuStateMachine _mainMenuStateMachine;

        [Inject]
        public void Construct(StateFactory stateFactory, MainMenuStateMachine gameplayStateMachine)
        {
            _stateFactory = stateFactory;
            _mainMenuStateMachine = gameplayStateMachine;
        }

        public MainMenuStateMachine Init()
        {
            _mainMenuStateMachine.RegisterState(_stateFactory.CreateState<MainMenuInitState>());

            _mainMenuStateMachine.EnterIn<MainMenuInitState>();

            return _mainMenuStateMachine;
        }
    }
}
