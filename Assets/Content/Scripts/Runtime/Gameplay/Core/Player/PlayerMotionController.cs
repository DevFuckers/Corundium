using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    public class PlayerMotionController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private Vector3 _currentMoveDirection;
        private Transform _transform;
        private Vector3 _currentVelocity;
        private float _yRotation;

        private void Start()
        {
            if (_characterController == null)
            {
                throw new System.Exception("PlayerMotionController::Start() CharacterController is not assigned in the inspector.");
            }
            
            _transform = _characterController.transform;   
        }

        public void Move(Vector3 inputMoveDirection, float speed, float smoothMoveDeltaTime = 0.25f)
        {
            Vector3 moveVector = _transform.TransformDirection(new Vector3(inputMoveDirection.x, 0, inputMoveDirection.y))
            .normalized;

            // _currentMoveDirection.y = _jumpForce;
            _currentMoveDirection = Vector3.SmoothDamp(_currentMoveDirection, moveVector * speed, ref _currentVelocity,
                smoothMoveDeltaTime);

            _characterController.Move(_currentMoveDirection * Time.deltaTime);
        }

        public void SetRotationDirection(Vector2 rotationDirection, float rotationSpeed, Transform cameraObject)
        {
            rotationDirection = rotationDirection * rotationSpeed * Time.deltaTime;

            _yRotation -= rotationDirection.y;
            _yRotation = Mathf.Clamp(_yRotation, -70f, 90f);

            cameraObject.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
            _transform.Rotate(Vector3.up * rotationDirection.x);
        }
    }
}
