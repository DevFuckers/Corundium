
using System;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Curtain;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.UIRoot
{
    public class UIRootView : MonoBehaviour
    {
        public event Action ExitGameRequested;
        
        [SerializeField] private LoadingCurtain _curtain;
        [SerializeField] private Transform _uiSceneContainer;

        private void Awake()
        {
            DontDestroyOnLoad(this);   
        }

        public void ShowLoadingCurtain()
        {
            _curtain.Show();
        }

        public void HideLoadingCurtain()
        {
            _curtain.Hide();
        }

        public void AttachSceneUI(GameObject sceneUI)
        {
            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        public void ClearSceneUI()
        {
            var childCount = _uiSceneContainer.childCount;

            for (var i = 0; i < childCount; i++)
            {
                Destroy(_uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}
