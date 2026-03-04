using System;
using DG.Tweening;
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
		[SerializeField] private Transform _visualModel; // Ссылка на модель для DOTween эффектов

		[SyncVar(hook = nameof(OnHealthChanged))]
		private int _currentHealth;
		
		private Vector3 _originalScale;
		
		void Start()
		{
			_currentHealth = _maxHealth;
		}
		
		private void OnDestroy()
		{
			_visualModel?.DOKill();
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
				
				Die();
			}
		}
		
		[Server]
		private void Die()
		{
			// Здесь можно добавить логику выпадения лута перед удалением
			// SpawnResources();
			
			NetworkServer.Destroy(gameObject);
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
			
			if (_visualModel != null)
			{
				_visualModel.DOKill(true);
				
				_visualModel.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.2f, 10, 1f);
			}
		}
	}
}