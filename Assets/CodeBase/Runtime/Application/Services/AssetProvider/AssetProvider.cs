using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class AssetProvider : IAssetProvider
{
    public async Task<TAsset> Load<TAsset>(string path) where TAsset : class
    {
        var handle = Addressables.LoadAssetAsync<TAsset>(path);
        await handle.Task;
        return handle.Result; 
    }

    public async Task<TAsset> Load<TAsset>(AssetReference key) where TAsset : class
    {
        var handle = Addressables.LoadAssetAsync<TAsset>(key);
        await handle.Task;
        return handle.Result;
    }
}
