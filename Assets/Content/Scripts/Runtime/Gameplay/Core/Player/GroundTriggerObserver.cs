using System;
using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    [RequireComponent(typeof(Collider))]
    public class GroundTriggerObserver : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayerMask;
        public event Action<Collider> Enter;
        public event Action<Collider> Exit;

        public bool IsGrounded { get; private set; } = false;

        private void OnTriggerEnter(Collider other)
        {
            if ((_groundLayerMask.value & 1 << other.gameObject.layer) > 0) 
            {
                Enter?.Invoke(other);
                IsGrounded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((_groundLayerMask.value & 1 << other.gameObject.layer) > 0) 
            {
                Exit?.Invoke(other);
                IsGrounded = false;
            }
        }
    }
}
