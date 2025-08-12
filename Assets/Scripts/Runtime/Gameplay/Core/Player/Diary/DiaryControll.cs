using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary;
using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers
{

    // логика будет переписана, когда будет доступ к статическому PlayerInput
    public class DiaryControll : MonoBehaviour
    {
        public static DiaryControll Instance;

        private PlayerInput _inputActions;
        private GameObject _diaryObject;
        private DiaryTemplateCreator _diaryTemplateCreator;
        private DiaryTypeContent _currentPage;

        private Dictionary<DiaryTypeContent, GameObject> _pages = new Dictionary<DiaryTypeContent, GameObject>();


        private void Awake()
        {
            for (int i = 0; i < 1000; i++)
            {
                Debug.Log("тест");
            }
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
            _diaryObject = gameObject;
            if (_diaryObject == null)
            {
                Debug.Log("соси хуй");
            }

            _inputActions.Enable();
            _inputActions.Player.EnableDiary.performed += EnableDiary;
            _inputActions.UI.DisableDiary.performed += DisableDiary;
        }

        private void OnDestroy()
        {
            _inputActions.Disable();
            _inputActions.Player.EnableDiary.performed -= EnableDiary;
            _inputActions.UI.DisableDiary.performed -= DisableDiary;
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
            bool isContainsKey = false;
            isContainsKey = _pages.ContainsKey(typeContent);

            if (isContainsKey == false)
            {
                _pages.Add(typeContent, _diaryTemplateCreator.CreateTemplate(typeContent));
                return;
            }

            switch (typeContent)
            {
                case DiaryTypeContent.Craft:
                    _pages[typeContent].SetActive(true);
                    break;

                case DiaryTypeContent.Lighthouse:
                    _pages[typeContent].SetActive(true);
                    break;
            }
            if (_currentPage != DiaryTypeContent.empty)
            {
                _pages[typeContent].SetActive(false);
                _currentPage = typeContent;
            }
        }

     
    }
}
