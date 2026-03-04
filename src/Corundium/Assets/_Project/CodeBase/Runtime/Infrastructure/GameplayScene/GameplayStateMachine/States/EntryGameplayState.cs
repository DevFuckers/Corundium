using Zenject;

public class EntryGameplayState : IState
{
    private readonly SceneStateMachine _sceneStateMachine;
    private readonly IPauseService _pauseService;

    [Inject]
    public EntryGameplayState(SceneStateMachine sceneStateMachine, IPauseService pauseService)
    {
        _sceneStateMachine = sceneStateMachine;
        _pauseService = pauseService;
    }

    public void Enter()
    {
        // init services
        _pauseService.PerformResume();
        _sceneStateMachine.EnterIn<PlayGameplayState>();
    }

    public void Exit()
    {
    }
}
