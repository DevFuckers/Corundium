using DevFuckers._Project.CodeBase.Runtime.Common.Constants;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.EventBus;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.LoadingCurtain;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.SceneLoader;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using UnityEngine;
using Zenject;
using Event = CodeBase.Event;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States
{
    /// <summary>
    ///     Menu state responsible for loading the main menu scene and handling menu events.
    /// </summary>
    public class GameMenuState : IState
	{
		readonly EventBus _eventBus;
		readonly ILoadingCurtain _loadingCurtain;
		readonly ISceneLoader _sceneLoader;
		readonly GameStateMachine _stateMachine;

		[Inject]
		public GameMenuState(ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain, EventBus eventBus, GameStateMachine stateMachine)
		{
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_eventBus = eventBus;
			_stateMachine = stateMachine;
		}

		public void Enter()
		{
			Debug.Log("Menu  State");

			_sceneLoader.LoadScene(Scenes.MenuName);
			_loadingCurtain.Hide();

			_eventBus.Subscribe(Event.StartGameplay, GoToGameplay);
			_eventBus.Subscribe(Event.Quit, ExitMenu);
		}

		public void Exit()
		{
			Debug.Log("Exit Menu  State");

			_eventBus.Unsubscribe(Event.StartGameplay, GoToGameplay);
			_eventBus.Unsubscribe(Event.Quit, ExitMenu);
		}

		void GoToGameplay()
		{
			_stateMachine.EnterIn<GamePlayLoopState>();
		}

		void ExitMenu()
		{
			_stateMachine.EnterIn<GameExitState>();
		}
	}
}