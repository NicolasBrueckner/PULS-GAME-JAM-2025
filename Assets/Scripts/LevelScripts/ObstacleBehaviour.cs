#region

using UnityEngine;
using static Kaputt;

#endregion

public class ObstacleBehaviour : MonoBehaviour, IDestroyable
{
	public bool hasHealthUp;

	private LayerMask _playerLayer;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		_playerLayer = 1 << LayerMask.NameToLayer( "Player" );
	}

	private void OnTriggerEnter( Collider other )
	{
		if( !IsInLayerMask( other.gameObject, _playerLayer ) )
			return;

		_gem.OnPlayerHit();

		if( hasHealthUp )
			_gem.OnPlayerHeal();
	}

	public void DestroyInterfaceMember()
	{
		gameObject.SetActive( false );
	}
}