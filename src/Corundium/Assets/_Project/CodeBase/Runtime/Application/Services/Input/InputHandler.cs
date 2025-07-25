using System;
using UnityEngine;
using Zenject;

public class InputHandler : IInputHandler
{
    // Gameplay - Player
    public event Action<Vector2> PlayerMoveInputChanged = delegate { };
    public event Action<bool> JumpInputPressed = delegate { };
    public event Action AttackPerformed = delegate { };
    public event Action GetToolPerformed = delegate { };
    public event Action RadialMenuPerformed = delegate { };
    public event Action RadialMenuClosed = delegate { };
    public event Action ExitVehiclePressed = delegate { };

    public event Action InventoryPerformed = delegate { };

    public bool IsMovementInputLocked { get; set; }

    // Emergency
    public event Action<Vector2> RotateInputChanged = delegate { };
    public event Action InteractPerformed = delegate { };
    public event Action EscPerformed = delegate { };

    // Transport
    public event Action<Vector2> TransportMoveInputChanged = delegate { };

    private Input _input;
    public Input Input => _input ??= new Input();

    public void Enable()
    {
        Input.Gameplay.Move.performed += ctx => OnPlayerMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled += ctx => OnPlayerMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed += ctx => JumpInputPressed?.Invoke(true);
        Input.Gameplay.Jump.canceled += ctx => JumpInputPressed?.Invoke(false);

        Input.Gameplay.Attack.performed += ctx => AttackPerformed?.Invoke();
        Input.Gameplay.GetTool.performed += ctx => GetToolPerformed?.Invoke();

        Input.Gameplay.OpenRadialMenu.performed += ctx => RadialMenuPerformed?.Invoke();
        Input.Gameplay.OpenRadialMenu.canceled += ctx => RadialMenuClosed?.Invoke();

        Input.Gameplay.ExitTransport.performed += ctx => ExitVehiclePressed?.Invoke();
        
        Input.Emergency.Rotate.performed += ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());
        Input.Emergency.Interact.performed += ctx => InteractPerformed?.Invoke();
        Input.Emergency.Esc.performed += ctx => EscPerformed?.Invoke();

        Input.Transport.Move.performed += ctx => OnTransportMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Transport.Move.canceled += ctx => OnTransportMoveInputChanged(Vector2.zero);


        Input.Gameplay.Inventory.performed += _ => InventoryPerformed?.Invoke();

        Input.Enable();
        Input.Transport.Disable();
    }

    public void Disable()
    {
        Input.Disable();

        Input.Gameplay.Move.performed -= ctx => OnPlayerMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Gameplay.Move.canceled -= ctx => OnPlayerMoveInputChanged(Vector2.zero);

        Input.Gameplay.Jump.performed -= ctx => JumpInputPressed?.Invoke(true);
        Input.Gameplay.Jump.canceled -= ctx => JumpInputPressed?.Invoke(false);

        Input.Gameplay.Attack.performed -= ctx => AttackPerformed?.Invoke();

        Input.Gameplay.GetTool.performed -= ctx => GetToolPerformed?.Invoke();


        Input.Gameplay.OpenRadialMenu.performed -= ctx => RadialMenuPerformed?.Invoke();
        Input.Gameplay.OpenRadialMenu.canceled -= ctx => RadialMenuClosed?.Invoke();

        Input.Emergency.Rotate.performed -= ctx => OnRotateInputChanged(ctx.ReadValue<Vector2>());
        Input.Emergency.Interact.performed -= ctx => InteractPerformed?.Invoke();
        Input.Emergency.Esc.performed -= ctx => EscPerformed?.Invoke();

        Input.Transport.Move.performed -= ctx => OnTransportMoveInputChanged(ctx.ReadValue<Vector2>());
        Input.Transport.Move.canceled -= ctx => OnTransportMoveInputChanged(Vector2.zero);
    }

    private void OnRotateInputChanged(Vector2 direction)
    {
        if (IsMovementInputLocked == false)
            RotateInputChanged?.Invoke(direction);
    }

    private void OnPlayerMoveInputChanged(Vector2 direction)
    {
        if (IsMovementInputLocked == false)
            PlayerMoveInputChanged?.Invoke(direction);
    }

    private void OnTransportMoveInputChanged(Vector2 direction)
    {
        TransportMoveInputChanged?.Invoke(direction);
    }
}