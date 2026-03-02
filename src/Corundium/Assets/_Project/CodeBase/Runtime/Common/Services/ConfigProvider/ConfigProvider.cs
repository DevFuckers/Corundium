using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.ConfigProvider
{
    public class ConfigProvider : IConfigProvider
    {
        public async Task<T> GetConfigAsync<T>(string key = "default") where T : ScriptableObject
        {
            string addressKey = key == "default" ? $"{typeof(T).Name}_{key}" : key;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(addressKey);

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }

            Debug.LogError($"ConfigProvider::GetConfigAsync() Не удалось загрузить конфиг с ключом: {key}");
            return null;
        }
    }
}
