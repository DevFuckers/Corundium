using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSceneStateMachine();
        BindUIManager();
        BindPauseService();
        BindStateFactory();  // i don't have a clue why i need to register this shit twice, please shoot me
    }

    private void BindSceneStateMachine() => 
        Container.Bind<SceneStateMachine>().AsSingle();

    private void BindStateFactory() =>
        Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
    
    private void BindPauseService() =>
        Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
    
    private void BindUIManager() =>
        Container.Bind<UIManager>().AsSingle();
}