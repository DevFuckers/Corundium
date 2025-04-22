using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.CommonServices.Camera
{
    public class CameraService : ICameraService
    {
        private GameObject _motorCamera;
        private GameObject _followCamera;
        private GameObject _actionCamera;

        public void InitMotorCamera(GameObject cameraObject)
        {
            _motorCamera = cameraObject;
        }

        public void InitFollowCamera(GameObject cameraObject)
        {
            _followCamera = cameraObject;
        }

        public void InitActionCamera(GameObject cameraObject)
        {
            _actionCamera = cameraObject;
        }

        public GameObject GetMotorCamera()
        {
            return _motorCamera;
        }

        public GameObject GetFollowCamera()
        {
            return _followCamera;
        }

        public GameObject GetActionCamera()
        {
            return _actionCamera;
        }

        public void ShakeCamera(float intensity, float duration)
        {
            // Implement shake logic here
            Debug.Log($"Shaking camera with intensity: {intensity} for duration: {duration}");
        }
    }
}
