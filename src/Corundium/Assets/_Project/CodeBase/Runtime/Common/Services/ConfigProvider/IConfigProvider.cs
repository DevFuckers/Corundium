using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.ConfigProvider
{
	public interface IConfigProvider
	{
		/// <summary>
		/// method finds ScriptableObject asset with Addressabless mark 
		/// </summary>
		/// <param name="key">Adress key from addressabless</param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		UniTask<T> GetConfigAsync<T>(string key = "default") where T : ScriptableObject;

		UniTask<IList<T>> GetAllConfigsFromFolderAsync<T>(string folderKey) where T : ScriptableObject;
	}
}