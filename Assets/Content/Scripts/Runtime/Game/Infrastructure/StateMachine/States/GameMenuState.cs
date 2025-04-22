using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine.States
{
    public class GameMenuState : IState
    {
        // [Inject] private ISceneLoader _sceneLoader;
        [Inject] private GameStateMachine _gameStateMachine;

        public void Enter()
        {
            // затычка
            _gameStateMachine.EnterIn<GameplayState>();
        }

        public void Exit()
        {
        }
    }
}
