using System.Collections.Generic;
using System.Linq;
using DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback.Data;
using DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback.UI;
using UnityEngine;

namespace DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback
{
    public class DiaryFeedbackInstaller : MonoBehaviour
    {
        [SerializeField] private Transform _pagesRoot;
        [SerializeField] private GameObject _body;
        [SerializeField] private string _configPath = DiaryConfig.Name;

        private DiaryFeedbackInputHandler _inputHandler;
        private List<PageSwitchButton> _pageSwitchButtons;

        private IEnumerable<string> PageSwitchButtonsIds => 
            _pageSwitchButtons.Select(button => button.PageId);

        private void Awake()
        {
            RegisterPageSwitchButtons();

            var configProvider = new DiaryConfigProvider(_configPath);
            var pageCreator = new PageCreator(configProvider, _pagesRoot);
            var pageSwitcher = new PageSwitcher(pageCreator, PageSwitchButtonsIds);

            _inputHandler = new DiaryFeedbackInputHandler(new PlayerInput(), pageSwitcher, _body);

            ConstructPageSwitchButtons(pageSwitcher);
        }

        private void Start() =>
            _body.SetActive(false);

        private void OnEnable() =>
            _inputHandler.Enable();

        private void OnDisable() =>
            _inputHandler.Disable();

        private void RegisterPageSwitchButtons() => 
            _pageSwitchButtons = GetComponentsInChildren<PageSwitchButton>().ToList();

        private void ConstructPageSwitchButtons(PageSwitcher pageSwitcher) =>
            _pageSwitchButtons.ForEach(p => p.Construct(pageSwitcher));
    }
}