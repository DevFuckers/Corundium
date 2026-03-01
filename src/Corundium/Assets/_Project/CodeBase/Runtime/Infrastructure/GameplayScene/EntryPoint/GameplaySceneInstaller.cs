using DevFuckers._Project.CodeBase.Runtime.Common.Factories.StateFactory;
using DevFuckers._Project.CodeBase.Runtime.Features.Pause;
using DevFuckers._Project.CodeBase.Runtime.Features.UIManager;
using DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameplayScene.GameplayStateMachine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameplayScene.EntryPoint
{
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
            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle();
    
        private void BindUIManager() =>
            Container.Bind<UIManager>().AsSingle();
    }
}