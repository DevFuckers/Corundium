using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using DevFuckers._Project.CodeBase.Runtime.Features.Pause;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameplayScene.GameplayStateMachine.States
{
	/// <summary>
	/// Entry point for the gameplay state; initializes scene components and transitions to Play state.
	/// </summary>
	public class EntryGameplayState : IState
	{
		private readonly SceneStateMachine _sceneStateMachine;
		private readonly IPauseController _pauseController;

		[Inject]
		public EntryGameplayState(SceneStateMachine sceneStateMachine, IPauseController pauseController)
		{
			_sceneStateMachine = sceneStateMachine;
			_pauseController = pauseController;
		}

		public void Enter()
		{
			// init services
			_pauseController.PerformResume();
			_sceneStateMachine.EnterIn<PlayGameplayState>();
		}

		public void Exit()
		{
		}
	}
}