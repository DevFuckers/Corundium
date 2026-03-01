using DevFuckers._Project.CodeBase.Runtime.Common.Services.Cursor;
using DevFuckers._Project.CodeBase.Runtime.Common.Services.Input;
using DevFuckers._Project.CodeBase.Runtime.Features.Pause;
using DevFuckers._Project.CodeBase.Runtime.Features.Stamina;
using Mirror;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Actor
{
	public class ActorMotor : NetworkBehaviour, IPausable
	{
		[Header("References")] [SerializeField]
		private CharacterController _controller;

		[SerializeField] private Camera _camera;
		[SerializeField] private Renderer _model;
		[SerializeField] private Animator _animator;

		[Header("Settings")] [SerializeField] private float _gravity;
		[SerializeField] private float _jumpHeight;
		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotateSpeed;
		[SerializeField] private float _smoothMoveDeltaTime;

		private IInputHandler _inputHandler;
		private IStaminaSpender _staminaSpender;
		private IPauseController _pauseController;
		private Transform _motorObject;
		private Vector3 _currentMoveDirection;
		private Vector3 _newMoveDirection;
		private Vector3 _currentVelocity;
		private float _yRotation;
		private float _jumpForce;

		[SerializeField] private bool _isJumpActive = false;
		[SerializeField] private bool _isMoveActive = false;
		private bool _isRunActive = false;
		private bool _isPaused = false;

		[Inject]
		public void Construct(
			IInputHandler inputHandler,
			ICursorService cursorService,
			IStaminaSpender staminaSpender,
			IPauseController pauseController)
		{
			_staminaSpender = staminaSpender;
			_inputHandler = inputHandler;
			_pauseController = pauseController;
		}

		private void Start()
		{
			_isMoveActive = true;
			_motorObject = transform;

			OnEnable();

			if (!isLocalPlayer)
			{
				_camera.gameObject.SetActive(false);
			}

			if (isLocalPlayer)
			{
				_model.enabled = false;
			}
		}

		private void OnEnable()
		{
			if (!isLocalPlayer)
				return;

			if (_inputHandler == null)
				return;

			_inputHandler.RotateInputChanged += SetRotationDirection;
			_inputHandler.PlayerMoveInputChanged += SetMoveDirection;
			_inputHandler.JumpInputPressed += SetJumpActive;
			_inputHandler.RunInputPressed += SetRunActive;

			_pauseController.Add(this);
		}

		private void OnDisable()
		{
			if (!isLocalPlayer)
				return;

			if (_inputHandler == null)
				return;

			_inputHandler.RunInputPressed -= SetRunActive;
			_inputHandler.RotateInputChanged -= SetRotationDirection;
			_inputHandler.PlayerMoveInputChanged -= SetMoveDirection;
			_inputHandler.JumpInputPressed -= SetJumpActive;

			_pauseController.Remove(this);
		}

		private void Update()
		{
			if (!isLocalPlayer)
				return;

			UpdateGravity();

			if (_isJumpActive)
				AddJumpForce(_jumpHeight);

			if (_isMoveActive)
			{
				Move(_newMoveDirection);
				Animate();
			}
		}

		private void Move(Vector3 moveDirection)
		{
			float moveSpeedMultiplier = 1f;

			Vector3 moveVector = transform.TransformDirection(new Vector3(moveDirection.x, 0, moveDirection.y))
				.normalized;

			if (_isRunActive && moveVector.magnitude > 0.1f)
			{
				if (_staminaSpender.CanSpendFor(ESpendindStaminaType.Run, Time.deltaTime))
				{
					moveSpeedMultiplier = 2f;
					_staminaSpender.SpendFor(ESpendindStaminaType.Run, Time.deltaTime);
				}
				else
				{
					moveSpeedMultiplier = 0f;
				}
			}

			_currentMoveDirection.y = _jumpForce;

			_currentMoveDirection = Vector3.SmoothDamp(_currentMoveDirection,
				moveVector * (_moveSpeed * moveSpeedMultiplier), ref _currentVelocity,
				_smoothMoveDeltaTime);

			_controller.Move(_currentMoveDirection * Time.deltaTime);
		}

		private void SetRotationDirection(Vector2 rotation)
		{
			if (_isPaused)
				return;
		
			rotation = rotation * _rotateSpeed * Time.deltaTime;

			_yRotation -= rotation.y;
			_yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

			_camera.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
			_motorObject.Rotate(Vector3.up * rotation.x);
		}

		private void AddJumpForce(float value)
		{
			if (_isPaused)
				return;
		
			if (_controller.isGrounded && _staminaSpender.CanSpendFor(ESpendindStaminaType.Jump))
			{
				_jumpForce = value;
				_staminaSpender.SpendFor(ESpendindStaminaType.Jump);
			}
		}

		private void UpdateGravity()
		{
			if (_jumpForce > _gravity)
			{
				_animator.SetBool("IsnotGrounded", true);
				_jumpForce += _gravity * Time.deltaTime;
			}
			else
			{
				_animator.SetBool("IsnotGrounded", false);
			}
		}

		private void Animate() =>
			_animator.SetFloat("Velocity", _controller.velocity.magnitude);

		private void SetMoveDirection(Vector2 moveDirection)
		{
			if (_isPaused)
			{
				_newMoveDirection = Vector3.zero;
				return;
			}
		
			_newMoveDirection = moveDirection;
		}

		private void SetJumpActive(bool isJumpActive) =>
			_isJumpActive = isJumpActive;

		private void SetRunActive(bool isActive) =>
			_isRunActive = isActive;

		public void Stop()
		{
			_isPaused = true;
		}

		public void Resume()
		{
			_isPaused = false;
		}
	}
}