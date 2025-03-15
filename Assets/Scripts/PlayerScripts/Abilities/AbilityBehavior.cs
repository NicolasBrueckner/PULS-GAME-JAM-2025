#region

using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

#endregion

[ RequireComponent( typeof( Shockwave ) ) ]
[ RequireComponent( typeof( Bash ) ) ]
[ RequireComponent( typeof( Dash ) ) ]
public class AbilityBehavior : MonoBehaviour
{
	public SerializedDictionary<IAbility, bool> abilities = new();

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_gem.PlayerHit += OnPlayerHitReceived;
		_gem.PlayerHeal += OnPlayerHealReceived;
	}

	private void OnPlayerHitReceived()  => ChangeNextAbilityStatus( false );
	private void OnPlayerHealReceived() => ChangeNextAbilityStatus( true );

	private void ChangeNextAbilityStatus( bool isActive )
	{
		IAbility next = abilities.FirstOrDefault( pair => pair.Value != isActive ).Key;

		if( next == null )
			return;

		next.ChangeActivityStatus( isActive );
		abilities[ next ] = isActive;
	}
}