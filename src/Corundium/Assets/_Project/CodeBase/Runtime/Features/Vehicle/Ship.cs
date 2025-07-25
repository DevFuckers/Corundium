using Mirror;
using UnityEngine;

namespace DevFuckers
{
    public class Ship : NetworkBehaviour, IVehicle
    {
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _turnSpeed = 50f;
        [SerializeField] private NetworkIdentity _identity;
        [SerializeField] private Transform _driverPlace;
        private PlayerController _driver;
        private Vector3 _newMoveDirection;

        public Transform DriverPlace => _driverPlace;
        public NetworkIdentity Identity => _identity;
        public PlayerController GetDriver() => _driver;
        public void SetDriver(PlayerController value) => _driver = value;

        public void DisableControl(IInputHandler inputHandler)
        {
            inputHandler.PlayerMoveInputChanged -= SetMoveDirection;
        }

        public void EnableControl(IInputHandler inputHandler)
        {
            _newMoveDirection = Vector3.zero;
            inputHandler.PlayerMoveInputChanged += SetMoveDirection;
        }

        private void SetMoveDirection(Vector2 moveDirection)
        {
            _newMoveDirection = moveDirection;
        }

        void Update()
        {
            if (_driver == null || !isOwned)
                return; 

            float move = _newMoveDirection.y * _moveSpeed * Time.deltaTime;
            float turn = _newMoveDirection.x * _turnSpeed * Time.deltaTime;

            transform.Translate(Vector3.forward * move);
            transform.Rotate(Vector3.up * turn);
        }
    }
}
