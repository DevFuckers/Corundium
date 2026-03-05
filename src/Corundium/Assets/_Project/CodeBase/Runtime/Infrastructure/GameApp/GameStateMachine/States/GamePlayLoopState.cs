using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States
{
    /// <summary>
    ///     Core gameplay loop state where gameplay scene transitions and networking are coordinated.
    /// </summary>
    public class GamePlayLoopState : IState
	{
		public void Enter()
		{
			// scene change handle networkManager
		}

		public void Exit()
		{
		}
	}
}