using DevFuckers.Assets.Scripts.Runtime.Gameplay.Core.Player.Diary;
using UnityEngine;
using UnityEngine.UI;

namespace DevFuckers
{
    public class ContentButtonContext : MonoBehaviour
    {
        public DiaryTypeContent TypeContent => _typeContent;

        [SerializeField] private DiaryTypeContent _typeContent;

        private Button _contentButton;

        private void Awake()
        {
            _contentButton = GetComponent<Button>();
        }
        private void Start()
        {
            if (_typeContent != DiaryTypeContent.empty)
            {
                _contentButton.onClick.AddListener(() => DiaryControll.Instance.EnablePage(_typeContent));
            }
        }
    }
}
