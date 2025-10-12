using DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Installer
{
    public class StaminaInstaller : MonoInstaller, IStamina
    {
        public float Value = 100;
        public float RefillPerSecond = 20;
        public Slider View;
        public string StaminaSpendingConfigPath = StaminaSpendingConfig.Name;

        private Stamina _model;
        private StaminaRefiller _refiller;

        private IStaminaSpender _spender;

        float IStamina.Value
        {
            get => _model.Value;
            set => _model.Value = value;
        }

        public override void InstallBindings()
        {
            var configProvider = new StaminaSpendingConfigProvider(StaminaSpendingConfigPath);
            var model = new Stamina(Value, Value);
            var spender = new StaminaSpender(model, configProvider);
            var refiller = new StaminaRefiller(model, RefillPerSecond);

            _model = model;
            _refiller = refiller;

            Container.Bind<IStaminaSpender>().FromInstance(spender).AsSingle();
        }

        private void Start()
        {
            UpdateView();
            _model.Changed += UpdateView;
        }

        private void Update() =>
            _refiller.Update(Time.deltaTime);

        private void OnDestroy() =>
            _model.Changed -= UpdateView;

        private void UpdateView()
        {
            if (View != null)
                View.value = _model.Value / _model.MaxValue;
        }
    }
}