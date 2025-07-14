using UnityEngine;

public class CursorService : ICursorService
{
    public void SetCursorVisibility(bool visible)
    {
        Cursor.lockState = visible? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }
}
