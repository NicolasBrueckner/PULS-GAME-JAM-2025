#region

using Unity.Mathematics;
using UnityEngine;

#endregion


//used for any damage behaviour including invincibility
public class HealthComponent : MonoBehaviour
{
	public int maxHealth;
	public int Health{ get; private set; }
	public bool IsInvincible{ get; private set; }

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		Health = maxHealth;
	}

	private void Start()
	{
		_gem.PlayerHit += OnPlayerHitReceived;
		_gem.PlayerHeal += OnPlayerHealReceived;
		_gem.PlayerInvincible += OnPlayerInvincibleReceived;
	}

	private void OnPlayerHealReceived()
	{
		Health++;
		Health = math.min( Health, maxHealth );
		Debug.Log( Health );
	}

	private void OnPlayerHitReceived()
	{
		if( IsInvincible )
			return;

		TakeDamage( 1 );
		Debug.Log( Health );
	}

	private void OnPlayerInvincibleReceived( bool isInvincible )
	{
		IsInvincible = isInvincible;
	}

	private void TakeDamage( int damage )
	{
		Health -= damage;

		if( Health > 0 )
			return;

		_gem.OnPlayerDead();
	}
}