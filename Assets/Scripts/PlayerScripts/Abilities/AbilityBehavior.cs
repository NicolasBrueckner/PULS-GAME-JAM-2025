#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public class AbilityBehavior : MonoBehaviour
{
	public GameObject shockWaveGameObject;
	public GameObject bashObject;
	public GameObject dashObject;

	private Dictionary<IAbility, bool> _abilities;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_abilities = new()
		{
			{ shockWaveGameObject.GetComponent<IAbility>(), false },
			{ bashObject.GetComponent<IAbility>(), false },
			{ dashObject.GetComponent<IAbility>(), false },
		};

		_gem.PlayerHit += OnPlayerHitReceived;
		_gem.PlayerHeal += OnPlayerHealReceived;
	}

	private void OnPlayerHitReceived()  => ChangeNextAbilityStatus( false );
	private void OnPlayerHealReceived() => ChangeNextAbilityStatus( true );

	private void ChangeNextAbilityStatus( bool isActive )
	{
		IAbility next = _abilities.FirstOrDefault( pair => pair.Value != isActive ).Key;

		if( next == null )
			return;

		next.ChangeActivityStatus( isActive );
		_abilities[ next ] = isActive;
	}
}