#region

using System;
using UnityEngine;
using static Kaputt;

#endregion

public class GameplayEventManager : MonoBehaviour
{
	public static GameplayEventManager Instance;

	public event Action PlayerHit;
	public event Action<bool> PlayerInvincible; 

	private void Awake()
	{
		Instance = CreateSingleton( Instance, gameObject );
	}

	public void OnPlayerHit() => PlayerHit?.Invoke();
	public void OnPlayerInvincible() => PlayerInvincible?.Invoke( true );
}