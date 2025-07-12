namespace DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine
{
    public interface IInfrastructureStateMachine
    {
        void EnterIn<TState>() where TState : IState;
        void RegisterState<TState>(TState state) where TState : IState;
    }
}
