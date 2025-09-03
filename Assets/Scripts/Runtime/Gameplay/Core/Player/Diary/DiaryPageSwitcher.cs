using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.DiaryData;
using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary
{
    // логика будет переписана, когда будет доступ к статическому PlayerInput
    public class DiaryPageSwitcher
    {

        private List<ContentButtonContext> _orderedContentButtons;
        private DiaryTemplateCreator _diaryTemplateCreator;
        private DiaryTypeContent _currentPage;

        private static Dictionary<DiaryTypeContent, GameObject> _pages = new Dictionary<DiaryTypeContent, GameObject>();


        public DiaryPageSwitcher(DiaryTemplateCreator diaryTemplateCreator)
        {
            _diaryTemplateCreator = diaryTemplateCreator;
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

        public void SelectNextPageButton(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("BlaBlaBla");
            if (_currentPage == DiaryTypeContent.empty) { return; }
            int DiaryTypeContentIndex = (int)_currentPage;
            int DiaryTypeContentFirstPageIndex = 1;
            bool IsPageExists = Enum.IsDefined(typeof(DiaryTypeContent), DiaryTypeContentIndex + 1);
            DiaryTypeContent typeContent;

            if (IsPageExists == false)
            {
                typeContent = (DiaryTypeContent)DiaryTypeContentFirstPageIndex;
                EnablePage(typeContent);
            }
            else
            {
                typeContent = (DiaryTypeContent)DiaryTypeContentIndex + 1;
                EnablePage(typeContent);
            }
        } 

        public void SelectPreviousPageButton(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("BlaBlaBla1");
            if (_currentPage == DiaryTypeContent.empty) { return; }
            int DiaryTypeContentIndex = (int)_currentPage;
            int DiaryTypeContentLastPageIndex = Enum.GetValues(typeof(DiaryTypeContent)).Length;
            bool IsPageExists = Enum.IsDefined(typeof(DiaryTypeContent), DiaryTypeContentIndex - 1);
            DiaryTypeContent typeContent;

            if (IsPageExists == false)
            {
                typeContent = (DiaryTypeContent)DiaryTypeContentLastPageIndex;
                EnablePage(typeContent);
            }
            else
            {
                typeContent = (DiaryTypeContent)DiaryTypeContentIndex - 1;
                EnablePage(typeContent);
            }
        }

    }
}
