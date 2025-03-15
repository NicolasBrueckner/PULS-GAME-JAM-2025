#region

using UnityEngine;

#endregion


//used for any damage behaviour including invincibility
public class HealthComponent : MonoBehaviour
{
	public int maxHealth;
	public int Health{ get; private set; }
	public bool IsInvincible { get; private set; }

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		Health = maxHealth;
	}

	private void Start()
	{
		_gem.PlayerHit += OnPlayerHitReceived;
		_gem.PlayerInvincible += OnPlayerInvincibleReceived;
	}

	private void OnPlayerHitReceived()
	{
		TakeDamage( 1 );
	}

	private void OnPlayerInvincibleReceived(bool isInvincible)
	{
		IsInvincible = isInvincible;
	}

	private void TakeDamage( int damage )
	{
		Health -= damage;
	}
}