#region

using System;
using UnityEngine;
using static Kaputt;

#endregion

public class GameplayEventManager : MonoBehaviour
{
	public static GameplayEventManager Instance;

	public event Action GameStarted;
	public event Action GameEnded;
	public event Action PlayerHit;
	public event Action PlayerHeal;
	public event Action<bool> PlayerInvincible;
	public event Action<float> MoveSpeedChange;

	private void Awake()
	{
		Instance = CreateSingleton( Instance, gameObject );
	}


	public void OnGameStarted()                         => GameStarted?.Invoke();
	public void OnGameEnded()                           => GameEnded?.Invoke();
	public void OnPlayerHit()                           => PlayerHit?.Invoke();
	public void OnPlayerHeal()                          => PlayerHeal?.Invoke();
	public void OnPlayerInvincible( bool isInvincible ) => PlayerInvincible?.Invoke( isInvincible );
	public void OnMoveSpeedChange( float factor )       => MoveSpeedChange?.Invoke( factor );
}