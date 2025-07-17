using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuUIObject;

    [Header("UI Components")]
    [SerializeField] private Button _exitGameplayButton;
    private IInputHandler _inputHandler;
    private ICursorService _cursorService;
    private SceneStateMachine _sceneStateMachine;
    private bool _isMenuActive;

    [Inject]
    public void Construct(IInputHandler inputHandler, ICursorService cursorService, SceneStateMachine stateMachine)
    {
        _inputHandler = inputHandler;
        _cursorService = cursorService;
        _sceneStateMachine = stateMachine;
    }

    private void Start()
    {
        _inputHandler.EscPerformed += ResetMenuBool;

        _exitGameplayButton.onClick.AddListener(_sceneStateMachine.EnterIn<ExitGameplayState>);

        _isMenuActive = false;
        UpdateMenuVisible();
    }

    private void OnDestroy()
    {
        _inputHandler.EscPerformed -= ResetMenuBool;

        _exitGameplayButton.onClick.RemoveAllListeners();
    }

    private void UpdateMenuVisible()
    {
        _menuUIObject.SetActive(_isMenuActive);

        _cursorService.SetCursorVisibility(_isMenuActive);
    }

    private void ResetMenuBool()
    {
        _isMenuActive = !_isMenuActive;

        UpdateMenuVisible();
    }
}
