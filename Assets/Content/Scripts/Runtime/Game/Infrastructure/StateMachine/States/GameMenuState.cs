using System.Threading.Tasks;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.SceneLoader;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.UIRoot;
using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Helpers;
using DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.EntryPoint;
using DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine.States
{
    public class GameMenuState : IState
    {
        [Inject] private readonly ISceneLoader _sceneLoader;
        [Inject] private readonly UIRootView _uIRootView;
        [Inject] private readonly GameStateMachine _gameStateMachine;

        public async void Enter()
        {
            await _sceneLoader.LoadAsync(Scenes.MAIN_MENU);;

            while (Object.FindFirstObjectByType<MainMenuStateMachineInit>() == null)
            {
                if (Time.timeSinceLevelLoad > 10f)
                    break; // заменить на выход в error state

                await Task.Yield();
            }

            MainMenuStateMachine mainMenuStateMachine = Object.FindFirstObjectByType<MainMenuStateMachineInit>().Init();
            mainMenuStateMachine.StateMachineWasFinishedWithHostStarted += OnMainMenuStateMachineFinished;
        }

        private void OnMainMenuStateMachineFinished()
        {
            _uIRootView.ShowLoadingCurtain();
            _uIRootView.ClearSceneUI();

            _gameStateMachine.EnterIn<GameplayState>();
            
        }

        public void Exit()
        {
        }
    }
}
