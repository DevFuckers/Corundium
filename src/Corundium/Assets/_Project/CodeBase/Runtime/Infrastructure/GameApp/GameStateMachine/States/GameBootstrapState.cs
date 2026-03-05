using DevFuckers._Project.CodeBase.Runtime.Common.Services.Input;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.LoadingCurtain;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States
{
	/// <summary>
	///     Bootstrap state: sets up initial services and transitions to the main menu.
	/// </summary>
	public class GameBootstrapState : IState
	{
		readonly GameStateMachine _gameStateMachine;
		readonly InputHandler _inputHandler;
		readonly ILoadingCurtain _loadingCurtain;

		[Inject]
		public GameBootstrapState(GameStateMachine gameStateMachine, InputHandler inputHandler, ILoadingCurtain loadingCurtain)
		{
			_gameStateMachine = gameStateMachine;
			_inputHandler = inputHandler;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter()
		{
			Debug.Log("Boostrap State");

			Application.targetFrameRate = 60;

			// init services 
			_inputHandler.Enable();
			_loadingCurtain.Show();

			_gameStateMachine.EnterIn<GameMenuState>();
		}

		public void Exit()
		{
		}
	}
}