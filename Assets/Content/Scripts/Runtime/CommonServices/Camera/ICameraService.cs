using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera
{
    public interface ICameraService
    {
        void InitMotorCamera(GameObject cameraObject);
        void InitFollowCamera(GameObject cameraObject);
        void InitActionCamera(GameObject cameraObject);
        void SetCameraPosition(Vector3 position);
        void ShakeCamera(float intensity, float duration);
    }
}
