using UnityEngine;
using UnityEngine.SceneManagement;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameRunner
{
    public class GameRunner
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void InitBootstrapScene()
        {
            if (!IsEnabled())
                return;

            if (SceneManager.GetActiveScene().buildIndex == 0)
                return;

            SceneManager.LoadScene(0);
        }

        private static bool IsEnabled()
        {
            var config = Resources.Load<GameRunnerConfig>("GameRunnerConfig");

            if (config == null)
            {
                Debug.LogWarning("GameRunnerConfig не найден в папке Resources! Проверьте путь.");
                return true;
            }
            
            return config.Enabled;
        }
    }
}