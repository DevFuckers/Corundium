using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.AssetProvide
{
    public interface IAssetLoader
    {
        T Load<T>(string path) where T : Object;
        void Unload<T>(string path) where T : Object;
    }
}
