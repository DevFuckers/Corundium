using DevFuckers.Assets.Content.Scripts.Runtime.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.EntryPoint
{
    public static class GameRunner
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartGame()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == Scenes.BOOT)
                return;

            else
                SceneManager.LoadScene(Scenes.BOOT); 
        }
    }
}
