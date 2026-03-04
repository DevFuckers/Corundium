using Mirror;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.HealthSystem
{
	public class DamageableTest : NetworkBehaviour
	{
		[SerializeField] private TriggerObserver.TriggerObserver _triggerObserver;

		private void Start()
		{
			_triggerObserver.TriggerEnter += OnTriggerEnterHandler;
		}
	
		public void DealDamage(GameObject target, int damage)
		{
			if (!isServer) 
				return;

			if (target.TryGetComponent(out PlayerHealth health))
				health.TakeDamage(damage);
		}

		void OnTriggerEnterHandler(Collider other)
		{
			DealDamage(other.gameObject, 10);
		}
	}
}
