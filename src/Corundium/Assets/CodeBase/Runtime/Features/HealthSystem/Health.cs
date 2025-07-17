using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
	[SyncVar(hook = "OnHealthChanged")]
	[SerializeField, Min(1)] private int _currentHealth;
	[SerializeField, Min(1)] private int _maxHealth;
	
	[SerializeField] private WorldHealthBarUI _worldHealthBarUI;
	[SerializeField] private LocalPlayerHealthBarUI _localPlayerHealthBarUI;

	public override void OnStartServer()
	{
		_currentHealth = _maxHealth;
	}

	public override void OnStartClient()
	{
		if (!isLocalPlayer && _localPlayerHealthBarUI != null)
			_localPlayerHealthBarUI.gameObject.SetActive(false);
			
		if (isLocalPlayer && _worldHealthBarUI != null)
			_worldHealthBarUI.gameObject.SetActive(false);

		if (_worldHealthBarUI != null)
            _worldHealthBarUI.UpdateUI(_currentHealth, _maxHealth);
	}

	[Server]
    public void TakeDamage(int amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;
    }

	private void OnHealthChanged(int oldHealth, int newHealth)
	{
		Debug.Log($"Health changed: {oldHealth} → {newHealth}");
		
		_worldHealthBarUI.UpdateUI(newHealth, _maxHealth);
		_localPlayerHealthBarUI.UpdateUI(newHealth, _maxHealth);
	}
}