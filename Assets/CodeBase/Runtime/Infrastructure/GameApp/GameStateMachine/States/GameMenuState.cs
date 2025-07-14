using UnityEngine;
using Zenject;

public class GameMenuState : IState
{
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _loadingCurtain;

    [Inject]
    public GameMenuState(ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain)
    {
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }

    public void Enter()
    {
        Debug.Log("Menu  State");
        
        _sceneLoader.LoadScene(Scenes.MenuName);
        _loadingCurtain.Hide();
    }

    public void Exit()
    {
        Debug.Log("Exit Menu  State");
    }
}
