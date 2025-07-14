using UnityEngine;
using Zenject;

public class GameBootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly InputHandler _inputHandler;
    private readonly ILoadingCurtain _loadingCurtain;

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
        // init services 
        _inputHandler.Enable();
        _loadingCurtain.Show();

        _gameStateMachine.EnterIn<GameMenuState>();
    }

    public void Exit()
    {
        
    }
}
