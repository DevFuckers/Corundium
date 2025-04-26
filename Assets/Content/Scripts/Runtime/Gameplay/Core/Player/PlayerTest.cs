using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.ConfigProvide;
using DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Input;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.ActionStateMachine;
using DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player.MotorStateMachine;
using UnityEngine;
using UnityHFSM;
using Zenject;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    public class PlayerTest : MonoBehaviour
    {
        [SerializeField] private PlayerMotionController _playerMotionController; // test
        [SerializeField] private PlayerAnimationController _playerAnimationController; // test
        [SerializeField] private GameObject _rootCamera;
        [SerializeField] private GameObject _walkCamera;

        [Inject] private ICameraService _cameraService;
        [Inject] private ConfigProvider _configProvider;
        [Inject] private IInputService _inputService;
        private StateMachine _motorFSM;
        private StateMachine _actionFSM;

        public void Init()
        {
            _cameraService.InitRootCamera(_rootCamera);
            _cameraService.InitWalkCamera(_walkCamera);
            _inputService.Enable();

            Cursor.lockState = CursorLockMode.Locked; // test
            Cursor.visible = false; // test

            _configProvider.LoadAll();

            SetMotorStateMachine();
            SetActionStateMachine();

            _motorFSM.Init();
            _actionFSM.Init();
        }

        private void SetMotorStateMachine()
        {
            _motorFSM = new StateMachine();
            _motorFSM.AddState("Idle", new IdleState(_configProvider, _inputService, _cameraService, _playerMotionController, _playerAnimationController, needsExitTime: false, isGhostState: false));
            _motorFSM.AddState("Walk", new WalkState(_configProvider, _inputService, _cameraService, _playerMotionController, _playerAnimationController, needsExitTime: false, isGhostState: false));
            _motorFSM.AddState("Jump", new JumpState(_configProvider, _inputService, _cameraService, _playerMotionController, _playerAnimationController, needsExitTime: false, isGhostState: false));
            _motorFSM.AddState("Run", new RunState(_configProvider, _inputService, _cameraService, _playerMotionController, _playerAnimationController, needsExitTime: false, isGhostState: false));

            _motorFSM.AddTwoWayTransition("Idle", "Walk", t => IsMoving());
            _motorFSM.AddTwoWayTransition("Walk", "Run", t => IsRunning());

            _motorFSM.AddTransition("Walk", "Jump", t => IsReadyToJump());
            _motorFSM.AddTransition("Idle", "Jump", t => IsReadyToJump());

            _motorFSM.AddTransition("Jump", "Idle", t => _playerMotionController.IsPlayerGrounded());

            _motorFSM.SetStartState("Idle");
        }

        private void SetActionStateMachine()
        {
            _actionFSM = new StateMachine();

            _actionFSM.AddState("Attack", new AttackState(_playerAnimationController, _configProvider, needsExitTime: false, isGhostState: false));
            _actionFSM.AddState("Calm", new CalmState(_playerAnimationController, _configProvider, needsExitTime: false, isGhostState: false));

            // _actionFSM.AddTriggerTransition("AttackT", new Transition("Calm", "Attack"));
            _actionFSM.AddTriggerTransition("AttackT", "Calm", "Attack");
            _actionFSM.AddTriggerTransition("AttackT", new TransitionAfter( "Attack", "Calm", 0.5f));
            // _actionFSM.AddTransition(new TransitionAfter( "Attack", "Calm", 0.5f)); // вохможно делай начинается только после выполнения условия или начала перехода

            _actionFSM.SetStartState("Calm");

            _inputService.AttackInputPressed += TriggerAttackState;
        }

        private void TriggerAttackState()
        {
            _actionFSM.Trigger("AttackT");
            Debug.Log("Attack triggered!");
        }

        private void OnDestroy()
        {
            _inputService.AttackInputPressed -= TriggerAttackState;
        }

        private bool IsMoving()
        {
            return _inputService.CurrentMoveInput != Vector2.zero;
        }

        private bool IsRunning()
        {
            return _inputService.RunState;
        }

        private bool IsReadyToJump()
        {
            if (_inputService.JumpState && _playerMotionController.IsPlayerGrounded())
            {
                return true;
            }
            
            return false;
        }

        private void Update()
        {
            if (_motorFSM == null)
                return;
                
            _motorFSM.OnLogic();
            _actionFSM.OnLogic();
        }
    }
}
