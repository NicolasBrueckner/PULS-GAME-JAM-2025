#region

using System.Collections;
using Unity.Mathematics;
using UnityEngine;

#endregion


//used for any damage behaviour including invincibility
public class HealthComponent : MonoBehaviour
{
	public int maxHealth;
	public float invincibilityDuration;
	public GameObject hitEffect;
	public GameObject healEffect;
	public int Health{ get; private set; }
	public bool IsInvincible{ get; private set; }

    public AudioClip hitProjectileSound;
    public AudioClip hitObstacleSound;
    public AudioClip healSound;
    public AudioSource audioSource;

    private Coroutine _afterDamageCoroutine;
	private Coroutine _afterHealCoroutine;
	private const int AbilityHealthThreshold = 5;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		Health = maxHealth;
	}

	private void Start()
	{
		_gem.PlayerHit += OnPlayerHitReceived;
        _gem.PlayerCriticalHit += OnPlayerCriticalHitReceived;
        _gem.PlayerHeal += OnPlayerHealReceived;
		_gem.PlayerInvincible += OnPlayerInvincibleReceived;
    }

	private void OnPlayerHealReceived()
	{
		audioSource.clip = healSound;
		audioSource.Play();

		TakeHeal( 2 );
		_afterHealCoroutine ??= StartCoroutine( AfterHeal() );
	}

	private void OnPlayerHitReceived()
	{
		if( IsInvincible )
			return;

        audioSource.clip = hitObstacleSound;
        audioSource.Play();

        TakeDamage( 1 );
        _afterDamageCoroutine ??= StartCoroutine( AfterDamage() );
	}

    private void OnPlayerCriticalHitReceived()
    {
        if (IsInvincible)
            return;

        audioSource.clip = hitProjectileSound;
        audioSource.Play();

        TakeDamage(2);
		_afterDamageCoroutine ??= StartCoroutine(AfterDamage());
    }

    private void OnPlayerInvincibleReceived( bool isInvincible )
	{
		IsInvincible = isInvincible;
	}

	private IEnumerator AfterDamage()
	{
		_gem.OnPlayerInvincible( true );
		float timer = invincibilityDuration;

		while( ( timer -= Time.fixedDeltaTime ) > 0f )
		{
			hitEffect.SetActive( true );

			if( timer <= invincibilityDuration / 3 )
				hitEffect.SetActive( false );

			yield return new WaitForFixedUpdate();
		}
        if ((maxHealth - Health) % AbilityHealthThreshold == 0)
            _gem.OnPlayerHealthThresholdReached();

		_gem.OnPlayerInvincible( false );
		_afterDamageCoroutine = null;
	}

	private IEnumerator AfterHeal()
	{
		float timer = 1;
		healEffect.SetActive( true );

		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();

		healEffect.SetActive( false );
		_afterHealCoroutine = null;
	}

	private void TakeDamage( int damage )
	{
		_afterDamageCoroutine ??= StartCoroutine( AfterDamage() );

		Health -= damage;

		if( Health > 0 )
			return;

		_gem.OnPlayerDead();
	}

	private void TakeHeal( int healAmount )
	{
		Health += healAmount;
		Health = math.min( Health, maxHealth );
    }
}