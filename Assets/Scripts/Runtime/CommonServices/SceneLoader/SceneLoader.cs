using System.Threading.Tasks;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.SceneLoader;
using UnityEngine.SceneManagement;

namespace VB.Assets.Content.Scripts.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public Task LoadAsync(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
            return Task.CompletedTask;
        }
    }
}
