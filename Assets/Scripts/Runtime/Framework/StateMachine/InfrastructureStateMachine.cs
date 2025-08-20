using System;
using System.Collections.Generic;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine
{
    public class InfrastructureStateMachine : IInfrastructureStateMachine
    {
        private Dictionary<Type, IState> _statesMap;
        private IState _currentState;

        public InfrastructureStateMachine()
        {
            _statesMap = new Dictionary<Type, IState>();
        }

        public void EnterIn<TState>() where TState : IState
        {
            if (_statesMap.TryGetValue(typeof(TState), out IState state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }

        public void RegisterState<TState>(TState state) where TState : IState
        {
            if (_statesMap.ContainsKey(typeof(TState)))
                throw new ArgumentException("State already existing in States Map: " + typeof(TState));  

            _statesMap.Add(typeof(TState), state);      
        }
    }
}
