using DevFuckers._Project.CodeBase.Runtime.Common.Constants;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.LoadingCurtain;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.SceneLoader;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.StateMachine;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.GameStateMachine.States
{
    public class GameMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;

        [Inject]
        public GameMenuState(ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            Debug.Log("Menu  State");
        
            _sceneLoader.LoadScene(Scenes.MenuName);
            _loadingCurtain.Hide();
        }

        public void Exit()
        {
            Debug.Log("Exit Menu  State");
        }
    }
}
