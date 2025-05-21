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

        private string _swingEvent = "SwingEvent";
        private string _attackEvent = "AttackEvent";
        private string _hitEvent = "HitEvent";
        private string _swingEndEvent = "SwingEndEvent";

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
            _motorFSM.AddState("Hit", new HitState(_configProvider, _cameraService, _playerMotionController, _playerAnimationController, needsExitTime: false, isGhostState: false));
            _motorFSM.AddState("Grounding", new GroundingState(_configProvider, _inputService, _cameraService, _playerMotionController, needsExitTime: false, isGhostState: false));
            _motorFSM.AddState("Dead", new DeadState(needsExitTime: false, isGhostState: false));
            _motorFSM.AddState("InAir", new InAirState(_configProvider, _inputService, _cameraService, _playerMotionController, needsExitTime: false, isGhostState: false));
            

            _motorFSM.AddTwoWayTransition("Idle", "Walk", t => IsMoving());
            _motorFSM.AddTwoWayTransition("Walk", "Run", t => IsRunning());

            _motorFSM.AddTransition("Walk", "Jump", t => IsReadyToJump());
            _motorFSM.AddTransition("Idle", "Jump", t => IsReadyToJump());

            // тут не понятно как сделать
            _motorFSM.AddTransition(new TransitionAfter("Jump", "InAir", 0.2f)); // переключает в состояние InAir после 2 секунд (если прыжок дольше чем 1+ секунды)
            // _motorFSM.AddTransition(new TransitionAfter("Jump", "Idle", 0.2f)); // переключает в состояние Idle после 1 секунды (если прыжок не дольше чем 1 секунда)

            _motorFSM.AddTransition("InAir", "Grounding", t => _playerMotionController.IsPlayerGrounded());
            _motorFSM.AddTransition(new TransitionAfter("Grounding", "Idle", 0.3f));

            _motorFSM.AddTriggerTransitionFromAny(_hitEvent, new TransitionBase("", "Hit"));

            _motorFSM.SetStartState("Idle");
        }

        private void SetActionStateMachine()
        {
            _actionFSM = new StateMachine();

            var attackState = new AttackState(_playerAnimationController, _configProvider, needsExitTime: false, isGhostState: false);
            var swingState = new SwingState(_playerAnimationController, _configProvider, needsExitTime: false, isGhostState: false);

            _actionFSM.AddState("Attack", attackState);
            _actionFSM.AddState("Calm", new CalmState(_playerAnimationController, _configProvider, needsExitTime: false, isGhostState: false));
            _actionFSM.AddState("Swing", swingState);

            _actionFSM.AddTriggerTransition(_swingEvent, "Calm", "Swing");
            _actionFSM.AddTriggerTransition(_swingEndEvent, "Swing", "Attack");
            _actionFSM.AddTriggerTransition(_attackEvent, "Attack", "Calm");

            _actionFSM.SetStartState("Calm");

            _inputService.AttackInputPressed += TriggerSwingState;
            attackState.OnAttackEvent += TriggerAttackState; // где-то отписку вставить
            swingState.OnSwingEndEvent += TriggerSwingEndState; // где-то отписку вставить
        }

        private void TriggerSwingState()
        {
            _actionFSM.Trigger(_swingEvent);
            Debug.Log("Attack swing triggered!");
        }

        private void TriggerSwingEndState()
        {
            _actionFSM.Trigger(_swingEndEvent);
            Debug.Log(" swing end triggered!");
        }

        private void TriggerAttackState()
        {
            _actionFSM.Trigger(_attackEvent);
            Debug.Log("Attack triggered!");
        }

        private void OnDestroy()
        {
            _inputService.AttackInputPressed -= TriggerSwingState;
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
