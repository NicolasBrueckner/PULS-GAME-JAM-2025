#region

using UnityEngine;

#endregion

public class HealthComponent : MonoBehaviour
{
	public int maxHealth;
	public int Health{ get; private set; }

	private GameplayEventManager _gem;

	private void Awake()
	{
		Health = maxHealth;
	}

	private void Start()
	{
		_gem.PlayerHit += OnPlayerHitReceived;
	}

	private void OnPlayerHitReceived()
	{
		TakeDamage( 1 );
	}

	private void TakeDamage( int damage )
	{
		Health -= damage;
	}
}