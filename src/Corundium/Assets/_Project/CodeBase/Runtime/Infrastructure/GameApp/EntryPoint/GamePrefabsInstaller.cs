using UnityEngine;
using Zenject;

public class GamePrefabsInstaller : MonoInstaller
{
    [SerializeField] private CustomNetworkManager _networkManagerPrefab;
    [SerializeField] private Curtain _loadingCurtain;

    public override void InstallBindings()
    {
        BindNetworkManager();
        BindLoadingCurtain();
    }

    private void BindNetworkManager()
    {
        Container
            .Bind<CustomNetworkManager>()
            .FromComponentInNewPrefab(_networkManagerPrefab)
            .AsSingle()
            .NonLazy();
    }

    private void BindLoadingCurtain()
    {
        Container
            .BindInterfacesAndSelfTo<Curtain>()
            .FromComponentInNewPrefab(_loadingCurtain)
            .AsSingle();
    }
}
