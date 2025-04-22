using System.Threading.Tasks;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.SceneLoader
{
    public interface ISceneLoader
    {
        void Load(string sceneName);
        Task LoadAsync(string sceneName);
    }
}