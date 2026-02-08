using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
	[SerializeField, Min(1)] private int _maxHealth;
	
	[SerializeField] private WorldHealthBarUI _worldHealthBarUI;
	[SerializeField] private LocalPlayerHealthBarUI _localPlayerHealthBarUI;
	
	[SyncVar(hook = "OnHealthChanged")]
	private int _currentHealth;

	public override void OnStartServer()
	{
		_currentHealth = _maxHealth;
	}

	public override void OnStartClient()
	{
		// выключаем у всех, кроме нашего клиента, локальный хп бар
		if (!isLocalPlayer && _localPlayerHealthBarUI != null)
			_localPlayerHealthBarUI.gameObject.SetActive(false);
			
		// отключаем глобальный хп бар только у себя, остальные могут видеть его
		if (isLocalPlayer && _worldHealthBarUI != null)
			_worldHealthBarUI.gameObject.SetActive(false);

		if (_worldHealthBarUI != null)
            _worldHealthBarUI.UpdateUI(_currentHealth, _maxHealth);
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
		
		_worldHealthBarUI.UpdateUI(newHealth, _maxHealth);
		_localPlayerHealthBarUI.UpdateUI(newHealth, _maxHealth);
	}
}