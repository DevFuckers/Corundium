using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;
    private StateFactory _stateFactory;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine, StateFactory stateFactory)
    {
        _gameStateMachine = gameStateMachine;
        _stateFactory = stateFactory;
    }

    private void Start()
    {
        _gameStateMachine.RegisterState(_stateFactory.Create<GameBootstrapState>());
        _gameStateMachine.RegisterState(_stateFactory.Create<GameMenuState>());
        _gameStateMachine.RegisterState(_stateFactory.Create<GamePlayLoopState>());

        _gameStateMachine.EnterIn<GameBootstrapState>();

        DontDestroyOnLoad(this);
    }
}
