using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Test
{
    public class StaminaEffector : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _value = 100;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Stamina stamina))
            {
                stamina.Value += _value;
            }
        }
    }
}