using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SetUpMenuUI : MonoBehaviour
{
    [SerializeField] private DropdownSceneSelector _sceneSelector;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _startHostButton;
    [SerializeField] private Button _startClientButton;
    [SerializeField] private TMP_InputField _inputAddressField;

    private CustomNetworkManager _networkManager;
    private OnlineSceneSetter _sceneSetter;
    private ExitGame _exitGame;

    [Inject]
    public void Construct(CustomNetworkManager networkManager)
    {
        _networkManager = networkManager;

        _sceneSetter = new OnlineSceneSetter(networkManager);
        _exitGame = new ExitGame();
    }

    private void OnEnable()
    {
        _sceneSelector.SceneSelected += _sceneSetter.SetOnlineScene;
        _exitButton.onClick.AddListener(_exitGame.CloseApp);
        
        _startHostButton.onClick.AddListener(_networkManager.StartHost);
        _startClientButton.onClick.AddListener(_networkManager.StartClient);
        _inputAddressField.onEndEdit.AddListener((string newAddress) => _networkManager.networkAddress = newAddress);
    }

    private void OnDisable()
    {
        _sceneSelector.SceneSelected -= _sceneSetter.SetOnlineScene;
        _exitButton.onClick.RemoveAllListeners();
        
        _startClientButton.onClick.RemoveAllListeners();
        _startHostButton.onClick.RemoveAllListeners();
        _inputAddressField.onEndEdit.RemoveAllListeners();
    }
}