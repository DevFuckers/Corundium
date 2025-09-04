using DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.UI
{
    public class DiaryInputHandler
    {
        private PlayerInput _playerInput;
        private GameObject _diaryBody;

        private Action<InputAction.CallbackContext> _enableDiary;
        private Action<InputAction.CallbackContext> _disableDiary;
        private Action<InputAction.CallbackContext> _switchToNextPage;
        private Action<InputAction.CallbackContext> _switchToPreviousPage;
        public DiaryInputHandler(PlayerInput playerInput, DiaryPageSwitcher pageSwitcher, GameObject diaryBody)
        {
            _playerInput = playerInput;
            _diaryBody = diaryBody;


            _switchToNextPage += pageSwitcher.SelectNextPageButton;
            _switchToPreviousPage += pageSwitcher.SelectPreviousPageButton;

            _enableDiary = _ =>
            {
                _diaryBody.SetActive(true);
                _playerInput.Player.Disable();
                _playerInput.UI.Enable();

            };

            _disableDiary = _ =>
            {
                _diaryBody.SetActive(false);
                _playerInput.Player.Enable();
                _playerInput.UI.Disable();

            };
        }

        public void Enable()
        {
            _playerInput.Enable();
            _playerInput.Player.EnableDiary.performed += _enableDiary;
            _playerInput.UI.DisableDiary.performed += _disableDiary;
            _playerInput.UI.SelectNextDiaryPageButton.performed += _switchToNextPage;
            _playerInput.UI.SelectPreviousDIaryPageButton.performed += _switchToPreviousPage;
        }

        public void Disable()
        {
            _playerInput.Disable();
            _playerInput.Player.EnableDiary.performed -= _enableDiary;
            _playerInput.UI.DisableDiary.performed -= _disableDiary;
            _playerInput.UI.SelectNextDiaryPageButton.performed -= _switchToNextPage;
            _playerInput.UI.SelectPreviousDIaryPageButton.performed -= _switchToPreviousPage;
        }
    }
}
