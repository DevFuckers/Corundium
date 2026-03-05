using DevFuckers._Project.CodeBase.Runtime.Common.Services.Input;
using DevFuckers._Project.CodeBase.Runtime.Features.HealthSystem;
using DevFuckers._Project.CodeBase.Runtime.Features.Pause;
using Mirror;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Actor
{
    public class PlayerAttack : NetworkBehaviour, IPausable
    {
        [SerializeField] private Camera _camera;
        [SerializeField, Min(0)] private int _damageAmount = 5;
        [SerializeField] private GameObject _hitEffectPrefab;

        private IInputHandler _inputHandler;
        private IPauseController _pauseController;
        private bool _isPaused;
            
        [Inject]
        public void Construct(IPauseController pauseController, IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _pauseController = pauseController;
        }
        
        public override void OnStartLocalPlayer()
        {
            if (_inputHandler == null)
            {
                Debug.LogError("Input Handler is missing!");
                return;
            }
            
            
            _inputHandler.AttackPerformed += OnAttackPerformed;
            _pauseController.Add(this);
        }
        
        void OnDisable()
        {
            if (isLocalPlayer && _inputHandler != null)
            {
                _inputHandler.AttackPerformed -= OnAttackPerformed;
                _pauseController.Remove(this);
            }
        }

        [Client]
        private void OnAttackPerformed()
        {
            if (_isPaused)
                return;
            
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
            OnAttackPerformedCmd(ray.origin, ray.direction);
        }
        
        [Command]
        void OnAttackPerformedCmd(Vector3 origin, Vector3 direction)
        {
            if (Physics.Raycast(origin, direction, out RaycastHit hit))
            {
                Debug.Log("Hit " + hit.transform.name);
                
                if (hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damageAmount);
                }
                
                ShowHitEffect(hit.point, hit.normal);
            }
        }
        
        [TargetRpc]
        private void ShowHitEffect(Vector3 pos, Vector3 normal)
        {
            // Спавним искры (выполняется только у себя)
            Instantiate(_hitEffectPrefab, pos, Quaternion.LookRotation(normal));
        }

        public void Stop()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }
    }
}
