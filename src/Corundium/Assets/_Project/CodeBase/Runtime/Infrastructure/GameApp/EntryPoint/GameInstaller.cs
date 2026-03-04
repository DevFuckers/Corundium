using DevFuckers;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCursorService();
        BindInputService();
        BindSceneLoader();
        BindAssetProvider();
        BindGameStateMachine();
        BindSceneProvider();
    }

    private void BindInputService() => 
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();

    private void BindSceneLoader() =>
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

    private void BindAssetProvider() =>
        Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
    
    private void BindGameStateMachine()
    {
        Container.Bind<StateFactory>().AsSingle();
        Container.Bind<GameStateMachine>().AsSingle();
    }

    private void BindCursorService() =>
        Container.BindInterfacesAndSelfTo<CursorService>().AsSingle();
    
    private void BindSceneProvider() =>
        Container.Bind<BuildScenesProvider>().AsSingle();
}