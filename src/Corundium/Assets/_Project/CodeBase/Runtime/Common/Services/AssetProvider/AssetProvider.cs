using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.AssetProvider
{
    /// <summary>
    ///     Asset provider abstraction over Addressables, enabling asynchronous asset loading.
    /// </summary>
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
}