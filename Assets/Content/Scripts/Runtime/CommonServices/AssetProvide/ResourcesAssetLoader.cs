using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.AssetProvide
{
    public class ResourcesAssetLoader : IAssetLoader
    {
        public T Load<T>(string path) where T : Object
        {
            // Implement loading logic here
            return Resources.Load<T>(path);
        }

        public void Unload<T>(string path) where T : Object
        {
            // Implement unloading logic here
            Resources.UnloadAsset(Resources.Load<T>(path));
        }
    }
}
