using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.BuildScenesProvider
{
    public class BuildScenesProvider
    {
        public List<string> Scenes => new(_scenes);

        private List<string> _scenes;

        public BuildScenesProvider()
        {
            _scenes = GetNames();
        }

        private List<string> GetNames()
        {
            int count = SceneManager.sceneCountInBuildSettings;
        
            List<string> result = new(count);

            for (int i = 0; i < count; i++)
                result.Add(GetName(i));

            return result;
        }

        private string GetName(int index)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(index);
            string filetype = ".scene";

            return path[..^filetype.Length]
                .Substring(path.LastIndexOf('/') + 1);
        }
    }
}