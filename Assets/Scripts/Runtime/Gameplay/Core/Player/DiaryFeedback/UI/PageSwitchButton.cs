using UnityEngine;
using UnityEngine.UI;

namespace DevFuckers.Runtime.Gameplay.Core.Player.DiaryFeedback.UI
{
    public class PageSwitchButton : MonoBehaviour
    {
        private Button _contentButton;
        private PageSwitcher _pageSwitcher;

        [field: SerializeField] public string PageId { get; private set; }

        public void Construct(PageSwitcher pageSwitcher) =>
            _pageSwitcher = pageSwitcher;

        private void Awake() =>
            _contentButton = GetComponent<Button>();

        private void Start()
        {
            _contentButton.onClick.AddListener(() =>
                _pageSwitcher.SwitchToPageBy(PageId));
        }

        private void OnDestroy() =>
            _contentButton.onClick.RemoveAllListeners();
    }
}