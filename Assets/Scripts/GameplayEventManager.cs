#region

using System;
using UnityEngine;
using static Kaputt;

#endregion

public class GameplayEventManager : MonoBehaviour
{
	public static GameplayEventManager Instance;

	public event Action GameStarted;
	public event Action GameWon;
	public event Action GameEnded;
    public event Action PlayerCriticalHit;
    public event Action PlayerHit;
    public event Action PlayerHealthThresholdReached;
    public event Action PlayerHeal;
	public event Action PlayerDead;
	public event Action<bool> PlayerInvincible;
	public event Action<float> MoveSpeedChange;
	public event Action<GameObject> ProjectileDeactivate;

	private void Awake()
	{
		Instance = CreateSingleton( Instance, gameObject );
	}

	public void OnGameStarted()                                 => GameStarted?.Invoke();
	public void OnGameWon()                                     => GameWon?.Invoke();
	public void OnGameEnded()                                   => GameEnded?.Invoke();
    public void OnPlayerCriticalHit()							=> PlayerCriticalHit?.Invoke();
    public void OnPlayerHit()                                   => PlayerHit?.Invoke();
    public void OnPlayerHealthThresholdReached()				=> PlayerHealthThresholdReached?.Invoke();
    public void OnPlayerHeal()                                  => PlayerHeal?.Invoke();
	public void OnPlayerDead()                                  => PlayerDead?.Invoke();
	public void OnPlayerInvincible( bool isInvincible )         => PlayerInvincible?.Invoke( isInvincible );
	public void OnMoveSpeedChange( float factor )               => MoveSpeedChange?.Invoke( factor );
	public void OnProjectileDeactivate( GameObject projectile ) => ProjectileDeactivate?.Invoke( projectile );
}