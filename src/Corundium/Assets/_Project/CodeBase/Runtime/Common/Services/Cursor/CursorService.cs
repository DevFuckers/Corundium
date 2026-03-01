using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Common.Services.Cursor
{
    public class CursorService : ICursorService
    {
        public void SetCursorVisibility(bool visible)
        {
            UnityEngine.Cursor.lockState = visible? CursorLockMode.None : CursorLockMode.Locked;
            UnityEngine.Cursor.visible = visible;
        }
    }
}
