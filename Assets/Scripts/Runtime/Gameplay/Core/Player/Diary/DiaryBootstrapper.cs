using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.DiaryData;
using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary.UI;
using System.Collections.Generic;
using UnityEngine;

namespace DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary
{
    public class DiaryBootstrapper : MonoBehaviour
    {
        public static DiaryPageSwitcher PageSwitcher;


        [SerializeField] private GameObject _diaryBody;
        [SerializeField] private Transform _pagesParent;
        [SerializeField] private PageData _pageData;

        private DiaryTemplateCreator _diaryTemplateCreator;
        private DiaryInputHandler _diaryInputHandler;
        private PlayerInput _playerInput;
        private DiaryPageDataProvider _diaryPageDataProvider;



        public void Awake()
        {
            _diaryPageDataProvider = new DiaryPageDataProvider(_pageData);
            _playerInput = new PlayerInput();
            _diaryTemplateCreator = new DiaryTemplateCreator(_pagesParent,_diaryPageDataProvider);
            PageSwitcher = new DiaryPageSwitcher(_diaryTemplateCreator);
            _diaryInputHandler = new DiaryInputHandler(_playerInput, PageSwitcher, _diaryBody);
        }

        private void OnEnable()
        {
            _diaryInputHandler.Enable();
        }

        private void OnDestroy()
        {
            _diaryInputHandler.Disable();
        }
    }
}
