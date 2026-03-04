using DevFuckers._Project.CodeBase.Runtime.Common.Services.LoadingCurtain;
using DevFuckers._Project.CodeBase.Runtime.Network;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Infrastructure.GameApp.EntryPoint
{
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
}
