using DevFuckers._Project.CodeBase.Runtime.Features.HealthSystem;
using Mirror;
using UnityEngine;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Actor
{
    public class PlayerAttack : NetworkBehaviour
    {
        [Inject] private IInputHandler _inputHandler;

        [SerializeField] private Camera _camera;
        [SerializeField, Min(0)] private int _damageAmount = 5;
        [SerializeField] private GameObject _hitEffectPrefab;

        public override void OnStartLocalPlayer()
        {
            if (_inputHandler == null)
            {
                Debug.LogError("Input Handler is missing!");
                return;
            }
            
            
            _inputHandler.AttackPerformed += OnAttackPerformed;
        }
        
        void OnDisable()
        {
            if (isLocalPlayer && _inputHandler != null)
            {
                _inputHandler.AttackPerformed -= OnAttackPerformed;
            }
        }

        [Client]
        private void OnAttackPerformed()
        {
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
    }
}
