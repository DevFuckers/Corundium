using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary;
using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers
{

    // логика будет переписана, когда будет доступ к статическому PlayerInput
    public class DiaryControll : MonoBehaviour
    {
        public static DiaryControll Instance;

        [SerializeField] private GameObject _diaryObject;
        [SerializeField] private List<ContentButtonContext> _orderedContentButtons;
        private PlayerInput _inputActions;
        private DiaryTemplateCreator _diaryTemplateCreator;
        private DiaryTypeContent _currentPage;

        private Dictionary<DiaryTypeContent, GameObject> _pages = new Dictionary<DiaryTypeContent, GameObject>();


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            _diaryTemplateCreator = GetComponent<DiaryTemplateCreator>();
            _inputActions = new PlayerInput();

            _inputActions.Enable();
            _inputActions.Player.EnableDiary.performed += EnableDiary;
            _inputActions.UI.DisableDiary.performed += DisableDiary;
            _inputActions.UI.SelectNextDiaryPageButton.performed += SelectNextPageButton;
            _inputActions.UI.SelectPreviousDIaryPageButton.performed += SelectPreviousPageButton;
        }

        private void OnDestroy()
        {
            _inputActions.Disable();
            _inputActions.Player.EnableDiary.performed -= EnableDiary;
            _inputActions.UI.DisableDiary.performed -= DisableDiary;
            _inputActions.UI.SelectNextDiaryPageButton.performed -= SelectNextPageButton;
            _inputActions.UI.SelectPreviousDIaryPageButton.performed -= SelectPreviousPageButton;
            _pages.Clear();
        }

        private void EnableDiary(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _diaryObject.SetActive(true);
            _inputActions.Player.Disable();
            _inputActions.UI.Enable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void DisableDiary(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _diaryObject.SetActive(false);
            _inputActions.Player.Enable();
            _inputActions.UI.Disable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void EnablePage(DiaryTypeContent typeContent)
        {
            if (typeContent != _currentPage)
            {

                bool isContainsKey = false;
                isContainsKey = _pages.ContainsKey(typeContent);

                if (isContainsKey == false)
                {
                    _pages.Add(typeContent, _diaryTemplateCreator.CreateTemplate(typeContent));
                }

                _pages[typeContent].SetActive(true);

                if (_currentPage != DiaryTypeContent.empty)
                {
                    _pages[_currentPage].SetActive(false);
                    _currentPage = typeContent;
                }

                else { _currentPage = typeContent; }

            }
        }

        private void SelectNextPageButton(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (_orderedContentButtons[_orderedContentButtons.Count - 1].TypeContent == _currentPage)
            {
                return;
            }

            for (int i = 0; i < _orderedContentButtons.Count; i++)
            {
                if (_currentPage == _orderedContentButtons[i].TypeContent)
                {
                    EnablePage(_orderedContentButtons[i +1].TypeContent);
                    return;
                }
            }
        }

        private void SelectPreviousPageButton(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (_orderedContentButtons[0].TypeContent == _currentPage)
            {
                return;
            }
            for (int i = 0; i < _orderedContentButtons.Count; i++)
            {
                if (_currentPage == _orderedContentButtons[i].TypeContent)
                {
                    EnablePage(_orderedContentButtons[i - 1].TypeContent);
                }
            }
        }

    }
}
