using System;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.Input
{
	public class InputHandler : IInputHandler, IDisposable
	{
		global::Input _input;

		public void Dispose()
		{
			if (_input != null)
			{
				_input.Disable();
				_input.Dispose();
				_input = null;
			}
		}

		// Gameplay - Player
		public event Action<bool> RunInputPressed = delegate { };
		public event Action<Vector2> PlayerMoveInputChanged = delegate { };
		public event Action<bool> JumpInputPressed = delegate { };
		public event Action AttackPerformed = delegate { };
		public event Action GetToolPerformed = delegate { };
		public event Action RadialMenuPerformed = delegate { };
		public event Action RadialMenuClosed = delegate { };

		public event Action InventoryPerformed = delegate { };

		public bool IsMovementInputLocked { get; set; }

		// Emergency
		public event Action<Vector2> RotateInputChanged = delegate { };
		public event Action InteractPerformed = delegate { };
		public event Action EscPerformed = delegate { };

		// Transport
		public event Action<Vector2> TransportMoveInputChanged = delegate { };
		public global::Input Input => _input ??= new global::Input();

		public void Enable()
		{
			Input.Gameplay.Run.performed += _ => RunInputPressed?.Invoke(true);
			Input.Gameplay.Run.canceled += _ => RunInputPressed?.Invoke(false);

			Input.Gameplay.Move.performed += ctx => OnPlayerMoveInputChanged(ctx.ReadValue<Vector2>());
			Input.Gameplay.Move.canceled += ctx => OnPlayerMoveInputChanged(Vector2.zero);

			Input.Gameplay.Jump.performed += ctx => JumpInputPressed?.Invoke(true);
			Input.Gameplay.Jump.canceled += ctx => JumpInputPressed?.Invoke(false);

			Input.Gameplay.Attack.performed += ctx => AttackPerformed?.Invoke();
			Input.Gameplay.GetTool.performed += ctx => GetToolPerformed?.Invoke();

			Input.Gameplay.OpenRadialMenu.performed += ctx => RadialMenuPerformed?.Invoke();
			Input.Gameplay.OpenRadialMenu.canceled += ctx => RadialMenuClosed?.Invoke();

			Input.Emergency.Rotate.performed += ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());
			Input.Emergency.Interact.performed += ctx => InteractPerformed?.Invoke();
			Input.Emergency.Esc.performed += ctx => EscPerformed?.Invoke();

			Input.Transport.Move.performed += ctx => OnTransportMoveInputChanged(ctx.ReadValue<Vector2>());
			Input.Transport.Move.canceled += ctx => OnTransportMoveInputChanged(Vector2.zero);

			Input.Gameplay.Inventory.performed += _ => InventoryPerformed?.Invoke();

			Input.Enable();
			Input.Transport.Disable();
		}

		void OnRotateInputChanged(Vector2 direction)
		{
			if (IsMovementInputLocked == false)
				RotateInputChanged?.Invoke(direction);
		}

		void OnPlayerMoveInputChanged(Vector2 direction)
		{
			if (IsMovementInputLocked == false)
				PlayerMoveInputChanged?.Invoke(direction);
		}

		void OnTransportMoveInputChanged(Vector2 direction)
		{
			TransportMoveInputChanged?.Invoke(direction);
		}
	}
}