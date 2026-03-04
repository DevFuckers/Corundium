using DevFuckers._Project.CodeBase.Runtime.Common.Factories.StateFactory;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameplayScene.GameplayStateMachine;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameplayScene.GameplayStateMachine.States;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameplayScene.EntryPoint
{
    public class GameplaySceneBootstrapper : MonoBehaviour
    {
        private SceneStateMachine _stateMachine;
        private StateFactory _stateFactory;

        [Inject]
        public void Construct(SceneStateMachine stateMachine, StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        private void Awake()
        {
            _stateMachine.RegisterState(_stateFactory.Create<EntryGameplayState>());
            _stateMachine.RegisterState(_stateFactory.Create<PlayGameplayState>());
            _stateMachine.RegisterState(_stateFactory.Create<ExitGameplayState>());

            _stateMachine.EnterIn<EntryGameplayState>();
        }
    }
}
