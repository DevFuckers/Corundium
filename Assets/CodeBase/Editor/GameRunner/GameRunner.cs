using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
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
            string path = "";

            var temp = Resources.LoadAll<GameRunnerConfig>(path);
            
            if (temp.Length == 0)
                return true;
            
            var config = temp[0];
            
            return config.Enabled;
        }
    }
}