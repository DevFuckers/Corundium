using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States
{
	public class GameExitState : IState
	{
		public void Enter()
		{
			Debug.Log("Exit  State");
        
			// здесь корректно выгружаем ресурсы
			Application.Quit();
		}

		public void Exit()
		{
		}
	}
}