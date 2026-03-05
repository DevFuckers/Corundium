using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.AssetProvider
{
	public interface IAssetProvider
	{
		Task<TAsset> Load<TAsset>(string path) where TAsset : class;
		Task<TAsset> Load<TAsset>(AssetReference key) where TAsset : class;
	}
}