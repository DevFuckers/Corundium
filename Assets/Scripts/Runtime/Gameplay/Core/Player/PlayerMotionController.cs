using UnityEngine;

namespace DevFuckers.Assets.Content.Scripts.Runtime.Gameplay.Core.Player
{
    public class PlayerMotionController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GroundTriggerObserver _groundTriggerObserver;
        private Transform _transform;
        private Vector3 _currentMoveDirection;
        private Vector3 _currentVelocity;
        private Vector3 _desiredBobOffset = Vector3.zero;
        private Vector3 _currentBobOffset;
        private float _yRotation;
        private float _jumpForce;

        private void Start()
        {
            if (_characterController == null)
            {
                throw new System.Exception("PlayerMotionController::Start() CharacterController is not assigned in the inspector.");
            }
            
            _transform = _characterController.transform;   
            _jumpForce = 0f;
            _currentMoveDirection = Vector3.zero;
        }

        public void Move(Vector3 inputMoveDirection, float speed, float smoothMoveDeltaTime = 0.25f)
        {
            Vector3 moveVector = _transform.TransformDirection(new Vector3(inputMoveDirection.x, 0, inputMoveDirection.y)).normalized;

            _currentMoveDirection.y = _jumpForce;

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

        public void UpdateGravity(float gravity = 16)
        {
            gravity = -gravity;
            if(_jumpForce > gravity)
            {
                _jumpForce += gravity * Time.deltaTime;
            } 
        }
        
        public void Jump(float jumpForce)
        {
            if (IsPlayerGrounded())
            {
                _jumpForce = jumpForce;
            }
        }

        public bool IsPlayerGrounded()
        {
            return _groundTriggerObserver.IsGrounded;
        }

        public void SetBobDesiredPosition(Vector2 bobDirection)
        {
            _desiredBobOffset = new Vector3(bobDirection.x, bobDirection.y, 0f);
        }

        public Vector3 UpdateBob(float bobbingFadeSpeed, float bobbingSetDestinationSpeed)
        {
            _desiredBobOffset = Vector3.Lerp(_desiredBobOffset, Vector3.zero, Time.deltaTime * bobbingSetDestinationSpeed * 0.5f);
            _currentBobOffset = Vector3.Lerp(_currentBobOffset, _desiredBobOffset, Time.deltaTime * bobbingFadeSpeed);
        
            return _currentBobOffset;
        }
    }
}
