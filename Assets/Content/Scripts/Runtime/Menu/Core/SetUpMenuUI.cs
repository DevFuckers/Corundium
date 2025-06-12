using DevFuckers.Assets.Content.Scripts.Runtime.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Menu.Core
{
    public class SetUpMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startHostButton; 
        [SerializeField] private Button _startClientButton; 
        [SerializeField] private TMP_InputField _inputAddressField; 
        private CustomNetworkManager _networkManager;

        [Inject]
        public void Construct(CustomNetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void Init()
        {
            if (_networkManager == null)
            {
                Debug.LogError("NetworkManager is not set up in SetUpMenuUI.");
                return;
            }
            
            _startHostButton.onClick.AddListener(_networkManager.StartHost);
            _startClientButton.onClick.AddListener(_networkManager.StartClient);
            _inputAddressField.onEndEdit.AddListener((newAddress) => _networkManager.networkAddress = newAddress);
        }

        private void OnDisable()
        {
            _startClientButton.onClick.RemoveAllListeners();
            _startHostButton.onClick.RemoveAllListeners();
            _inputAddressField.onEndEdit.RemoveAllListeners();
        }
    }
}
