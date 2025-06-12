using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.StateFactory;
using DevFuckers.Assets.Content.Scripts.Runtime.Menu.Core;
using DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.StateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Network;
using UnityEngine;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Menu.Infrastructure.EntryPoint
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private SetUpMenuUI _setUpMenuUI;
        [SerializeField] private CustomNetworkManager _customNetworkManager;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuStateMachine>().AsSingle().NonLazy();
            Container.Bind<StateFactory>().AsSingle();

            if (_setUpMenuUI == null)
            {
                Debug.LogError("SetUpMenuUI is not assigned in MainMenuInstaller.");
            }

            if (_customNetworkManager == null)
            {
                Debug.LogError("CustomNetworkManager is not assigned in MainMenuInstaller.");
            }

            Container.Bind<CustomNetworkManager>().FromInstance(_customNetworkManager).AsSingle().NonLazy();
            Container.Bind<SetUpMenuUI>().FromInstance(_setUpMenuUI).AsSingle().NonLazy();
        }
    }
}
