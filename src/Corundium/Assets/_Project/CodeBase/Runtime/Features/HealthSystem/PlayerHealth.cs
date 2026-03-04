using Mirror;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Features.HealthSystem
{
	public class PlayerHealth : NetworkBehaviour, IDamageable
	{
		[SerializeField, Min(1)] private int _maxHealth;
	
		[SerializeField] private WorldBarUI worldBarUI;
		[SerializeField] private LocalPlayerBarUI localPlayerBarUI;
	
		[SyncVar(hook = "OnHealthChanged")]
		private int _currentHealth;

		public override void OnStartServer()
		{
			_currentHealth = _maxHealth;
		}

		public override void OnStartClient()
		{
			// выключаем у всех, кроме нашего клиента, локальный хп бар
			if (!isLocalPlayer && localPlayerBarUI != null)
				localPlayerBarUI.gameObject.SetActive(false);
			
			// отключаем глобальный хп бар только у себя, остальные могут видеть его
			if (isLocalPlayer && worldBarUI != null)
				worldBarUI.gameObject.SetActive(false);

			if (worldBarUI != null)
				worldBarUI.UpdateUI(_currentHealth, _maxHealth);
		}

		[Server]
		public void TakeDamage(int amount)
		{
			if (_currentHealth <= 0) 
				return;

			_currentHealth -= amount;
		}

		private void OnHealthChanged(int oldHealth, int newHealth)
		{
			Debug.Log($"Health changed: {oldHealth} → {newHealth}");
		
			worldBarUI.UpdateUI(newHealth, _maxHealth);
			localPlayerBarUI.UpdateUI(newHealth, _maxHealth);
		}
	}
}