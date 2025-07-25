using System;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace DevFuckers
{

    public class PlayerInteract : NetworkBehaviour
    {
        public event Action<NetworkIdentity> InteractedWithTransport;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private float _maxDistanceRaycast = 3f;
        private RaycastHit _hitInfo;
        private IInputHandler _inputHandler;

        [Inject]
        public void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        void Start()
        {
            if (!isLocalPlayer)
                return;

            if (_inputHandler == null)
                return;

            _inputHandler.InteractPerformed += OnInteract;
        }

        void OnDestroy()
        {
            if (!isLocalPlayer)
                return;

            if (_inputHandler == null)
                return;

            _inputHandler.InteractPerformed -= OnInteract;
        }

        private void OnInteract()
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out _hitInfo, _maxDistanceRaycast))
            {
                if (_hitInfo.transform.TryGetComponent(out IVehicle interactable))
                {
                    InteractedWithTransport?.Invoke(interactable.Identity);
                }
            }
        }
    }
}
