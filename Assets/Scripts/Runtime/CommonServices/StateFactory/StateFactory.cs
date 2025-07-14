using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.StateFactory
{
    public class StateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TState CreateState<TState>() where TState : IState =>
            _instantiator.Instantiate<TState>();
    }
}
