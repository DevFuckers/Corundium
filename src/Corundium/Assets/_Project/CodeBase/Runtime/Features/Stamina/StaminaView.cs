using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public class StaminaView : MonoBehaviour 
    {
        [SerializeField] private LocalPlayerBarUI _uiBar;
        
        [Inject] private Stamina _model;
        
        private void Start() 
        {
            UpdateView();
            _model.Changed += UpdateView;
        }

        private void OnDestroy()
        {
            _model.Changed -= UpdateView;
        }

        private void UpdateView()
        {
            _uiBar.UpdateUI(_model.Value, _model.MaxValue);
        }
    }
}
