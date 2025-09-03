using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback
{
    public class DiaryFeedbackInputHandler
    {
        private PageSwitcher _pageSwitcher;
        private PlayerInput _input;
        private GameObject _diaryBody;

        private Action<InputAction.CallbackContext> _onDiaryOpened;
        private Action<InputAction.CallbackContext> _onDiaryClosed;
        private Action<InputAction.CallbackContext> _switchToNextPage;
        private Action<InputAction.CallbackContext> _switchToPreviousPage;

        public DiaryFeedbackInputHandler(PlayerInput input, PageSwitcher pageSwitcher, GameObject diaryBody)
        {
            _diaryBody = diaryBody;
            _input = input;
            _pageSwitcher = pageSwitcher;

            _onDiaryOpened = _ =>
            {
                _diaryBody.SetActive(true);
                
                _input.Player.Disable();
                _input.UI.Enable();
                
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            };
            
            _onDiaryClosed = _ =>
            {
                _diaryBody.SetActive(false);
                
                _input.Player.Enable();
                _input.UI.Disable();
                
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            };
            
            _switchToNextPage = _ => _pageSwitcher.SwitchToNextPage();
            _switchToPreviousPage = _ => _pageSwitcher.SwitchToPreviousPage();
        }

        public void Enable()
        {
            _input.Enable();
            _input.UI.Disable();
            
            _input.Player.EnableDiary.performed += _onDiaryOpened;
            _input.UI.DisableDiary.performed += _onDiaryClosed;
            _input.UI.SelectNextDiaryPageButton.performed += _switchToNextPage;
            _input.UI.SelectPreviousDIaryPageButton.performed += _switchToPreviousPage;
        }

        public void Disable()
        {
            _input.Disable();
            
            _input.Player.EnableDiary.performed -= _onDiaryOpened;
            _input.UI.DisableDiary.performed -= _onDiaryClosed;
            _input.UI.SelectNextDiaryPageButton.performed -= _switchToNextPage;
            _input.UI.SelectPreviousDIaryPageButton.performed -= _switchToPreviousPage;
        }
    }
}