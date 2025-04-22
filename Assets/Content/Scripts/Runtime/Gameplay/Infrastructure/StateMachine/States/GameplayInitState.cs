using UnityEngine;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.SceneLoader;
using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.StateMachine.States
{
    public class GameplayInitState : IState
    {
        [Inject] private ISceneLoader _sceneLoader;
        
        public void Enter()
        {
            PlayerTest g = Object.FindFirstObjectByType<PlayerTest>();
            g.Init();
        }

        public void Exit()
        {
        }
    }
}
