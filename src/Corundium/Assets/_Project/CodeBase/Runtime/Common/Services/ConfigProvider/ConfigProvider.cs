using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.ConfigProvider
{
	public class ConfigProvider : IConfigProvider
	{
		/// <summary>
		/// finds ScriptableObject asset with Addressabless mark 
		/// </summary>
		/// <param name="key">Adress key from addressabless</param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public async Task<T> GetConfigAsync<T>(string key = "default") where T : ScriptableObject
		{
			var addressKey = key == "default" ? $"{typeof(T).Name}_{key}" : key;

			var handle = Addressables.LoadAssetAsync<T>(addressKey);

			await handle.Task;

			if (handle.Status == AsyncOperationStatus.Succeeded) return handle.Result;

			Debug.LogError($"ConfigProvider::GetConfigAsync() Не удалось загрузить конфиг с ключом: {key}");
			return null;
		}

		public async Task<IList<T>> GetAllConfigsFromFolderAsync<T>(string folderKey) where T : ScriptableObject
		{
			if (string.IsNullOrEmpty(folderKey))
			{
				Debug.LogError("ConfigProvider::GetAllConfigsFromFolderAsync() Ключ папки пуст.");
				return null;
			}

			// LoadAssetsAsync загружает ВСЕ объекты, соответствующие ключу (адресу папки)
			// Вторая переменная (callback) может быть null, если нам не нужна поэтапная обработка
			var handle = Addressables.LoadAssetsAsync<T>(folderKey, null);
        
			await handle.Task;

			if (handle.Status == AsyncOperationStatus.Succeeded)
			{
				return handle.Result;
			}

			Debug.LogError($"ConfigProvider::GetAllConfigsFromFolderAsync() Ошибка загрузки из папки: {folderKey}");
			return null;
		}
	}
}