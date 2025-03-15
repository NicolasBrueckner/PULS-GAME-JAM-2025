#region

using System;
using UnityEngine;
using static Kaputt;

#endregion

public class GameplayEventManager : MonoBehaviour
{
	public static GameplayEventManager Instance;

	public event Action PlayerHit;
	public event Action PlayerHeal;
	public event Action<bool> PlayerInvincible;
	public event Action<float> MoveSpeedChange;

	private void Awake()
	{
		Instance = CreateSingleton( Instance, gameObject );
	}

	public void OnPlayerHit()                     => PlayerHit?.Invoke();
	public void OnPlayerHeal()                    => PlayerHeal?.Invoke();
	public void OnPlayerInvincible()              => PlayerInvincible?.Invoke( true );
	public void OnMoveSpeedChange( float factor ) => MoveSpeedChange?.Invoke( factor );
}