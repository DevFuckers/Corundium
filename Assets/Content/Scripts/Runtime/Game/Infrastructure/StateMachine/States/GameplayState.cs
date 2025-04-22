using System.Collections;
using System.Threading.Tasks;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.SceneLoader;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.UIRoot;
using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.EntryPoint;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] UIRootView _uIRootView;
        

        public async void Enter()
        {
            await _sceneLoader.LoadAsync(Scenes.GAMEPLAY);
            // Debug.Log(SceneManager.GetActiveScene().name);

            while (Object.FindFirstObjectByType<GameplayStateMachineInit>() == null)
            {
                if (Time.timeSinceLevelLoad > 10f)
                    break; // заменить на выход в error state

                await Task.Yield();
            }

            _uIRootView.HideLoadingCurtain();

            // yield return new WaitUntil(() => 
            //     Object.FindFirstObjectByType<GameplayStateMachineInit>() != null || Time.timeSinceLevelLoad > 20f);

            GameplayStateMachine gameplayStateMachine = Object.FindFirstObjectByType<GameplayStateMachineInit>().Init();
            gameplayStateMachine.StateMachineWasFinished += OnGameplayStateMachineFinished;
        }

        private void OnGameplayStateMachineFinished()
        {
            _uIRootView.ShowLoadingCurtain();
            _uIRootView.ClearSceneUI();
        }

        public void Exit()
        {
        }
    }
}
