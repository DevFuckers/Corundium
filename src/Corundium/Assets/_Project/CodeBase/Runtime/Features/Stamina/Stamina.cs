using System;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public class Stamina : IStamina
    {
        public event Action Changed;

        private float _maxValue;
        private float _value;

        public Stamina(float value, float maxValue)
        {
            _value = value;
            _maxValue = maxValue;
        }

        public float MaxValue => _maxValue;

        public float Value
        {
            get => _value;
            set
            {
                value = Mathf.Clamp(value, 0, _maxValue);

                if (value != _value)
                {
                    _value = value;
                    Changed?.Invoke();
                }
            }
        }
    }
}