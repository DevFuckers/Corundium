using UnityEngine.SceneManagement;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
