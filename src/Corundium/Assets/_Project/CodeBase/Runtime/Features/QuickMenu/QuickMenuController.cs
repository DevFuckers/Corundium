using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuickMenuController : MonoBehaviour
{
    [Inject] private IInputHandler _inputHandler;
    [Inject] private IPauseService _pauseService;
    [Inject] private ICursorService _cursorService;
    [Inject] private UIManager _uiManager;
    [Inject] private SceneStateMachine _sceneStateMachine;

    [SerializeField] private GameObject _quickMenuPanel;
    // [SerializeField] private Button _settingsButton;
    // [SerializeField] private Button _closeSettingsPanelButton;
    // [SerializeField] private GameObject _settingsPanelObject;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Button _continueButton;

    private bool _isMenuOpened = false;
    private bool _isSettingsPanelOpened = false;

    void OnValidate()
    {
        if (_exitGameButton == null)
            Debug.LogWarning("QuickMenuController::OnValidate() no exitGameButton setted in inspector");

        // if (_settingsButton == null)
        //     Debug.LogWarning("QuickMenuController::OnValidate() no settingsButton setted in inspector");
        //
        // if (_closeSettingsPanelButton == null)
        //     Debug.LogWarning("QuickMenuController::OnValidate() no closePanelButton setted in inspector");
        //
        // if (_settingsPanelObject == null)
        //     Debug.LogWarning("QuickMenuController::OnValidate() no settingsPanelObject setted in inspector");
    }

    private void Start()
    {
        // при старте игры выключаем курсор
        _cursorService.SetCursorVisibility(false);
    }
    
    void OnEnable()
    {
        // _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        // _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        //
        // _closeSettingsPanelButton.onClick.RemoveListener(OnCloseSettingsPanelButtonClicked);
        // _closeSettingsPanelButton.onClick.AddListener(OnCloseSettingsPanelButtonClicked);

        _exitGameButton.onClick.RemoveListener(OnExitGameButtonClicked);
        _exitGameButton.onClick.AddListener(OnExitGameButtonClicked);

        _continueButton.onClick.RemoveListener(OnContinueClicked);
        _continueButton.onClick.AddListener(OnContinueClicked);

        _inputHandler.EscPerformed -= OnEscPerformed;
        _inputHandler.EscPerformed += OnEscPerformed;
    }


    void OnDisable()
    {
        _exitGameButton.onClick.RemoveAllListeners();
        _continueButton.onClick.RemoveAllListeners();
        // _settingsButton.onClick.RemoveAllListeners();
        // _closeSettingsPanelButton.onClick.RemoveAllListeners();

        _inputHandler.EscPerformed -= OnEscPerformed;
    }

    private void OnEscPerformed()
    {
        if (_isMenuOpened)
        {
            CloseMenu();
            return;
        }

        if (_uiManager.CanOpenQuickMenu())
        {
            OpenMenu();
        }
    }

    private void CloseMenu()
    {
        _isSettingsPanelOpened = false;
        _isMenuOpened = false;

        // _settingsPanelObject.SetActive(false);
        _quickMenuPanel.SetActive(false);

        _uiManager.SetState(UIState.Normal);
        _pauseService.PerformResume();
        _cursorService.SetCursorVisibility(false);
    }

    private void OpenMenu()
    {
        _isSettingsPanelOpened = false;
        _isMenuOpened = true;

        // _settingsPanelObject.SetActive(false);
        _quickMenuPanel.SetActive(true);

        _pauseService.PerformStop();
        _uiManager.SetState(UIState.QuickMenuOpen);
        _cursorService.SetCursorVisibility(true);
    }

    private void OnContinueClicked()
        => CloseMenu();

    private void OnSettingsButtonClicked()
    {
        if (_isSettingsPanelOpened)
            return;

        // _settingsPanelObject.SetActive(true);
        _isSettingsPanelOpened = true;
        _uiManager.SetState(UIState.SettingsPanelOpen);
    }

    private void OnCloseSettingsPanelButtonClicked()
    {
        if (!_isSettingsPanelOpened)
            return;

        // _settingsPanelObject.SetActive(false);
        _isSettingsPanelOpened = false;
        _uiManager.SetState(UIState.QuickMenuOpen);
    }

    private void OnExitGameButtonClicked()
    {
        // _settingsPanelObject.SetActive(false);
        _isSettingsPanelOpened = false;
        _uiManager.SetState(UIState.Normal);

        _sceneStateMachine.EnterIn<ExitGameplayState>();
    }
}