using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Factories.StateFactory
{
    public class StateFactory
    {
        private IInstantiator _instantiator;

        [Inject]
        public StateFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;

        public TState Create<TState>() where TState : IState =>
            _instantiator.Instantiate<TState>();
    }
}