using Mirror;
using UnityEngine;

public class DamageableTest : NetworkBehaviour
{
	[SerializeField] private TriggerObserver _triggerObserver;

    private void Start()
    {
		_triggerObserver.TriggerEnter += OnTriggerEnter;
    }

    public void DealDamage(GameObject target, int damage)
	{
		if (!isServer) return;

		Debug.Log(target + " 2");

		if (target.TryGetComponent(out Health health))
			health.TakeDamage(damage);
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other + " 1");
		DealDamage(other.gameObject, 10);
	}
}
