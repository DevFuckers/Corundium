using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.AssetProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.StateFactory;
using DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine;
using VB.Assets.Content.Scripts.Infrastructure.Services.SceneLoader;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.EntryPoint
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoader();
            BindInputHandler();
            BindGameStateMachine();
            BindAssetLoader();
            BindConfigProvider();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<StateFactory>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }

        private void BindInputHandler() => 
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();

        private void BindSceneLoader() => 
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

        private void BindAssetLoader() => 
            Container.BindInterfacesAndSelfTo<ResourcesAssetLoader>().AsSingle();

        private void BindConfigProvider() => 
            Container.BindInterfacesAndSelfTo<ConfigProvider>().AsSingle();
    }
}
