using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera
{
    public interface ICameraService
    {
        void InitRootCamera(GameObject cameraObject);
        void InitWalkCamera(GameObject cameraObject);
        void InitActionCamera(GameObject cameraObject);
        
        GameObject GetRootCamera();
        GameObject GetWalkCamera();
        GameObject GetActionCamera();

        void ShakeCamera(float intensity, float duration);
    }
}
