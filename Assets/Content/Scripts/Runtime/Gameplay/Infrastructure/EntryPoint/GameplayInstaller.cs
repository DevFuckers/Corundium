using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.StateFactory;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.StateMachine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.EntryPoint
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameplayStateMachine();
            BindCameraService();
        }

        private void BindGameplayStateMachine()
        {
            Container.Bind<StateFactory>().AsSingle();
            Container.Bind<GameplayStateMachine>().AsSingle();
        }

        private void BindCameraService()
        {
            Container.Bind<ICameraService>().To<CameraService>().AsSingle();
        }
    }
}
