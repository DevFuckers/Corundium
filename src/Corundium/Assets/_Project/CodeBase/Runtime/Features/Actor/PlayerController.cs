using Mirror;
using UnityEngine;
using Zenject;

namespace DevFuckers
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private Transform _playerObject;
        [SerializeField] private ActorMotor _actorMotor;
        [SerializeField] private PlayerInteract _playerInteract;
        private IVehicle _currentVehicle;
        private IInputHandler _inputHandler;

        [Inject]
        public void Construct(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        private void Start()
        {
            if (!isOwned)
                return;

            _actorMotor.EnableMovementControl();
            _actorMotor.EnableRotationControl();

            _playerInteract.InteractedWithTransport += EnterVehicle;
            _inputHandler.ExitVehiclePressed += ExitVehicle;
        }

        void OnDestroy()
        {
            _playerInteract.InteractedWithTransport -= EnterVehicle;
            _inputHandler.ExitVehiclePressed -= ExitVehicle;
        }

        [Command]
        public void EnterVehicle(NetworkIdentity vehicleNetId)
        {
            var vehicle = vehicleNetId.GetComponent<IVehicle>();

            if (vehicle.GetDriver() == null)
            {
                vehicle.SetDriver(this);

                if (vehicleNetId.isOwned == false)
                {
                    vehicleNetId.AssignClientAuthority(connectionToClient);
                }

                RpcEnterVehicle(vehicleNetId);

                if (isLocalPlayer)
                {
                    DoEnterVehicleClientSide(vehicleNetId);
                }
            }
        }

        [Command]
        public void ExitVehicle()
        {
            if (_currentVehicle != null)
            {
                var vehicleNetId = _currentVehicle.Identity;
                _currentVehicle.SetDriver(null);

                if (_currentVehicle.Identity.isOwned == false)
                {
                    _currentVehicle.Identity.RemoveClientAuthority();
                }

                RpcExitVehicle(vehicleNetId);

                if (isLocalPlayer)
                {
                    DoExitVehicleClientSide(vehicleNetId);
                }

                _currentVehicle = null;
            }
        }

        [ClientRpc]
        void RpcExitVehicle(NetworkIdentity vehicleNetId)
        {
            if (!isLocalPlayer)
            {
                DoExitVehicleClientSide(vehicleNetId);
            }
        }
        
        [ClientRpc]
        void RpcEnterVehicle(NetworkIdentity vehicleNetId)
        {
            if (!isLocalPlayer)
            {
                DoEnterVehicleClientSide(vehicleNetId);
            }
        }

        void DoExitVehicleClientSide(NetworkIdentity vehicleNetId)
        {
            var vehicle = vehicleNetId.GetComponent<IVehicle>();

            _playerObject.position = vehicleNetId.transform.position + vehicleNetId.transform.right * 2f;
            _playerObject.SetParent(null);
            _actorMotor.EnableMovementControl();
            vehicle.DisableControl(_inputHandler);

            if (_currentVehicle != null && _currentVehicle.Identity == vehicleNetId)
            {
                _currentVehicle = null;
            }
        }

        void DoEnterVehicleClientSide(NetworkIdentity vehicleNetId)
        {
            _currentVehicle = vehicleNetId.GetComponent<IVehicle>();
            _playerObject.SetParent(_currentVehicle.DriverPlace);
            _playerObject.position = _currentVehicle.DriverPlace.position;
            _actorMotor.DisableMovementControl();
            _currentVehicle.EnableControl(_inputHandler);
        }
    }
}
