using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.UIRoot;
using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine.States
{
    public class GameInitState : IState
    {
        [Inject] private UIRootView _uIRootView;
        [Inject] private GameStateMachine _gameStateMachine;

        public void Enter()
        {
            _uIRootView.ShowLoadingCurtain();
        
            _gameStateMachine.EnterIn<GameMenuState>();
        }

        public void Exit()
        {
        }
    }
}
