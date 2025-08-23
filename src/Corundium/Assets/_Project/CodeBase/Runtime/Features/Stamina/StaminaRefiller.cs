namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public class StaminaRefiller : IUpdateable
    {
        private float _staminaPerSecond;
        private Stamina _stamina;

        private float _lastStaminaValue;
        private bool _isValueDecreasing;

        public StaminaRefiller(Stamina stamina, float staminaPerSecond)
        {
            _stamina = stamina;
            _staminaPerSecond = staminaPerSecond;
        }

        public void Update(float deltaTime)
        {
            _isValueDecreasing = _stamina.Value < _lastStaminaValue;

            if (_isValueDecreasing == false && _stamina.Value < _stamina.MaxValue) 
                _stamina.Value += _staminaPerSecond * deltaTime;
            
            _lastStaminaValue = _stamina.Value;
        }
    }
}