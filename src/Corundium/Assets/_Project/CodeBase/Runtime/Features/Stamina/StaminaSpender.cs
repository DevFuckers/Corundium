namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public class StaminaSpender : IStaminaSpender
    {
        private Stamina _stamina;
        private StaminaSpendingConfigProvider _configProvider;

        public StaminaSpender(Stamina stamina, StaminaSpendingConfigProvider configProvider)
        {
            _configProvider = configProvider;
            _stamina = stamina;
        }

        public float StaminaValue => _stamina.Value;

        public void SpendFor(ESpendindStaminaType spendingType)
        {
            float value = _configProvider.GetSpendingValueFor(spendingType);
            _stamina.Value -= value;
        }   
    
        public void SpendFor(ESpendindStaminaType spendingType, float duration)
        {
            float value = _configProvider.GetSpendingValueFor(spendingType);
            _stamina.Value -= value * duration;
        }
    
        public bool CanSpendFor(ESpendindStaminaType spendingType)
        {
            float value = _configProvider.GetSpendingValueFor(spendingType);
            return _stamina.Value >= value;
        }
    
        public bool CanSpendFor(ESpendindStaminaType spendingType, float duration)
        {
            float value = _configProvider.GetSpendingValueFor(spendingType);
            return _stamina.Value >= value * duration;
        }
    }
}