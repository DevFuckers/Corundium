using System;
using Mirror;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.HealthSystem
{
	[RequireComponent(typeof(NetworkAnimator))]
	[RequireComponent(typeof(NetworkTransformUnreliable))]
	public class HealthController : NetworkBehaviour, IDamageable
	{
		[SerializeField, Min(1)] private int _maxHealth;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private Animator _animator;

		[SyncVar(hook = nameof(OnHealthChanged))]
		private int _currentHealth;

		void Start()
		{
			_currentHealth = _maxHealth;
		}

		[Server]
		public void TakeDamage(int amount)
		{
			if (_currentHealth <= 0) 
				return;
			
			Debug.Log("Hit " + transform.name + " with " + amount);

			_currentHealth -= amount;

			if (_currentHealth <= 0)
			{
				Debug.Log("Destroy " + transform.name);
				// Логика ресурсов на сервере
				// SpawnResources();

				NetworkServer.Destroy(gameObject);
			}
		}

		private void OnHealthChanged(int oldHealth, int newHealth)
		{
			if (newHealth < oldHealth)
			{
				Debug.Log("play hit effects and so");
				PlayHitEffects();
			}
		}

		private void PlayHitEffects()
		{
			if (_audioSource != null)
				_audioSource.Play();

			if (_animator != null)
				_animator.SetTrigger("Hit");

			// Здесь можно спавнить частицы крови/искр локально на каждом клиенте
		}
	}
}