using UnityEngine;
using DevFuckers.Assets.Content.Scripts.Runtime.Framework.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player;
using System.Threading.Tasks;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Infrastructure.StateMachine.States
{
    public class GameplayInitState : IState
    {
        public async void Enter()
        {

            while (Object.FindFirstObjectByType<PlayerTest>() == null)
            {
                if (Time.timeSinceLevelLoad > 10f)
                    break; // заменить на выход в error state

                await Task.Yield();
            }

            PlayerTest g = Object.FindFirstObjectByType<PlayerTest>();

            g.Init();
        }

        public void Exit()
        {
        }
    }
}
