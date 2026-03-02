using DevFuckers._Project.CodeBase.Runtime.Common.Services.EventBus;
using DevFuckers._Project.CodeBase.Runtime.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Event = CodeBase.Event;

namespace DevFuckers._Project.CodeBase.Runtime.Features.ChooseGameplayScene
{
    public class SetUpMenuUI : MonoBehaviour
    {
        [SerializeField] private DropdownSceneSelector _sceneSelector;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Button _startHostButton;
        [SerializeField] private Button _startClientButton;
        [SerializeField] private TMP_InputField _inputAddressField;

        private CustomNetworkManager _networkManager;
        private EventBus _eventBus;

        [Inject]
        public void Construct(CustomNetworkManager networkManager, EventBus eventBus)
        {
            _networkManager = networkManager;
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _sceneSelector.SceneSelected += SetOnlineScene;
            _exitButton.onClick.AddListener(() => _eventBus.Trigger(Event.Quit));
        
            _startHostButton.onClick.AddListener(_networkManager.StartHost);
            _startClientButton.onClick.AddListener(_networkManager.StartClient);
            _inputAddressField.onEndEdit.AddListener((string newAddress) => _networkManager.networkAddress = newAddress);
        }

        private void OnDisable()
        {
            _sceneSelector.SceneSelected -= SetOnlineScene;
            _exitButton.onClick.RemoveAllListeners();
        
            _startClientButton.onClick.RemoveAllListeners();
            _startHostButton.onClick.RemoveAllListeners();
            _inputAddressField.onEndEdit.RemoveAllListeners();
        }

        private void SetOnlineScene(string sceneName)
        {
            _networkManager.onlineScene = sceneName;
        }
    }
}