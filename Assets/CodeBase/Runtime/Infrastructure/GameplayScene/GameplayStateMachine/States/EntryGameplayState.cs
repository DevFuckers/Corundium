using Zenject;

public class EntryGameplayState : IState
{
    private readonly SceneStateMachine _sceneStateMachine;

    [Inject]
    public EntryGameplayState(SceneStateMachine sceneStateMachine)
    {
        _sceneStateMachine = sceneStateMachine;
    }

    public void Enter()
    {
        // init services
        _sceneStateMachine.EnterIn<PlayGameplayState>();
    }

    public void Exit()
    {
    }
}
