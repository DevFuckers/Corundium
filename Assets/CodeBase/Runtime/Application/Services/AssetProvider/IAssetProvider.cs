using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAssetProvider
{
    //T Load<T>(string path) where T : Object;
    Task<TAsset> Load<TAsset>(string path) where TAsset : class;
    Task<TAsset> Load<TAsset>(AssetReference key) where TAsset : class;
}
