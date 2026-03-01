using DevFuckers._Project.CodeBase.Runtime.Common.Factories.StateFactory;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.AssetProvider;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.BuildScenesProvider;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.Cursor;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.Input;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.SceneLoader;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.EntryPoint
{
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
            Container.Bind<GameStateMachine.GameStateMachine>().AsSingle();
        }

        private void BindCursorService() =>
            Container.BindInterfacesAndSelfTo<CursorService>().AsSingle();
    
        private void BindSceneProvider() =>
            Container.Bind<BuildScenesProvider>().AsSingle();
    }
}