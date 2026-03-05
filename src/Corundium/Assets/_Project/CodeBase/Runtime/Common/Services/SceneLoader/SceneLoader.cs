using UnityEngine.SceneManagement;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.SceneLoader
{
    /// <summary>
    ///     Basic implementation of scene loading using Unity's SceneManager.
    /// </summary>
    public class SceneLoader : ISceneLoader
	{
        /// <summary>
        ///     Loads a scene by name using Unity's SceneManager.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}