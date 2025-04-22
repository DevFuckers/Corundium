using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera
{
    public interface ICameraService
    {
        void InitMotorCamera(GameObject cameraObject);
        void InitFollowCamera(GameObject cameraObject);
        void InitActionCamera(GameObject cameraObject);
        
        GameObject GetMotorCamera();
        GameObject GetFollowCamera();
        GameObject GetActionCamera();

        void ShakeCamera(float intensity, float duration);
    }
}
