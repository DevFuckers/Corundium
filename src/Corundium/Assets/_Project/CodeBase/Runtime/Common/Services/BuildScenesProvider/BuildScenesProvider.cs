using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.BuildScenesProvider
{
	public class BuildScenesProvider
	{
		readonly List<string> _scenes;

		public BuildScenesProvider()
		{
			_scenes = GetNames();
		}

		public List<string> Scenes => new(_scenes);

		List<string> GetNames()
		{
			var count = SceneManager.sceneCountInBuildSettings;

			List<string> result = new(count);

			for (var i = 0; i < count; i++)
				result.Add(GetName(i));

			return result;
		}

		string GetName(int index)
		{
			var path = SceneUtility.GetScenePathByBuildIndex(index);
			var filetype = ".scene";

			return path[..^filetype.Length]
				.Substring(path.LastIndexOf('/') + 1);
		}
	}
}