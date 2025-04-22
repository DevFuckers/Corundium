using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.UIRoot;
using UnityEngine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Game.Infrastructure.EntryPoint
{
    public class GamePrefabsInstaller : MonoInstaller
    {
        [SerializeField] private UIRootView _uIRootViewPrefab;
        
        public override void InstallBindings()
        {            
            BindUIRootView();
        }

        private void BindUIRootView()
        {
            UIRootView uIRootView = Container.InstantiatePrefabForComponent<UIRootView>(_uIRootViewPrefab);
            Container
                .Bind<UIRootView>()
                .FromInstance(uIRootView)
                .AsSingle();
        }
    }
}
