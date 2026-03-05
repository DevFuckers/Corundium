using DevFuckers._Project.CodeBase.Runtime.Common.Services.EventBus;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States
{
	/// <summary>
	///     Exit state: clean shutdown and prepare app termination.
	/// </summary>
	public class GameExitState : IState
	{
		readonly EventBus _eventBus;

		[Inject]
		public GameExitState(EventBus eventBus)
		{
			_eventBus = eventBus;
		}

		public void Enter()
		{
			Debug.Log("Exit  State");

			// здесь корректно выгружаем ресурсы
			_eventBus.Dispose();

			Application.Quit();
		}

		public void Exit()
		{
		}
	}
}