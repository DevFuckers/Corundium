using System;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.Input
{
    public interface IInputHandler
    {
        event Action<Vector2> RotateInputChanged;
        event Action<Vector2> PlayerMoveInputChanged;
        event Action<Vector2> TransportMoveInputChanged;
        event Action<bool> JumpInputPressed;
        event Action AttackPerformed;
        event Action GetToolPerformed;
        event Action InteractPerformed;
        event Action RadialMenuPerformed;
        event Action RadialMenuClosed;
    
        event Action EscPerformed;
    
        global::Input Input { get; }
        event Action InventoryPerformed;

        bool IsMovementInputLocked { get; set; }
        event Action<bool> RunInputPressed;
    }
}
