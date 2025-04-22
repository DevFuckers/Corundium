using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using UnityEngine;
using UnityHFSM;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    public class PlayerTest : MonoBehaviour
    {
        [SerializeField] private PlayerMotionController _playerMotionController; // test
        [SerializeField] private GameObject _motorCamera;

        [Inject] private ICameraService _cameraService;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private IInputService _inputService;
        private StateMachine _fsm;

        public void Init()
        {
            _cameraService.InitMotorCamera(_motorCamera);
            _inputService.Enable();

            Cursor.lockState = CursorLockMode.Locked; // test
            Cursor.visible = false; // test

            _configProvider.LoadAll();

            _fsm = new StateMachine();
            _fsm.AddState("Idle", new IdleState(_configProvider, _inputService, _cameraService, _playerMotionController, needsExitTime: false, isGhostState: false));
            _fsm.AddState("Walk", new WalkState(_configProvider, _inputService, _cameraService, _playerMotionController, needsExitTime: false, isGhostState: false));

            _fsm.AddTwoWayTransition("Idle", "Walk", t => IsMoving());

            _fsm.SetStartState("Idle");
            _fsm.Init();
        }

        private bool IsMoving()
        {
            return _inputService.CurrentMoveInput != Vector2.zero;
        }

        private void Update()
        {
            if (_fsm == null)
                return;
                
            _fsm.OnLogic();
        }
    }
}
