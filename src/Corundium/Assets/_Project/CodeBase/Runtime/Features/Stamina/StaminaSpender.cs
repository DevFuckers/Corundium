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

        public void SpendFor(string spendingId)
        {
            float value = _configProvider.GetSpendingValueFor(spendingId);
            _stamina.Value -= value;
        }   
    
        public void SpendFor(string spendingId, float duration)
        {
            float value = _configProvider.GetSpendingValueFor(spendingId);
            _stamina.Value -= value * duration;
        }
    
        public bool CanSpendFor(string spendingId)
        {
            float value = _configProvider.GetSpendingValueFor(spendingId);
            return _stamina.Value >= value;
        }
    
        public bool CanSpendFor(string spendingId, float duration)
        {
            float value = _configProvider.GetSpendingValueFor(spendingId);
            return _stamina.Value >= value * duration;
        }
    }
}