#region

using UnityEngine;
using static Kaputt;

#endregion

public class ObstacleBehaviour : MonoBehaviour, IDestroyable
{
	public bool hasHealthUp;
	public GameObject healEffect;

	private LayerMask _playerLayer;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		healEffect?.SetActive( hasHealthUp );
	}

	private void Awake()
	{
		_playerLayer = 1 << LayerMask.NameToLayer( "Player" );
	}

	private void OnTriggerEnter( Collider other )
	{
		if( !IsInLayerMask( other.gameObject, _playerLayer ) )
			return;
		Debug.Log( "player should take damage" );

		_gem.OnPlayerHit();
	}

	public void DestroyInterfaceMember()
	{
		if( hasHealthUp )
			_gem.OnPlayerHeal();

		gameObject.SetActive( false );
	}
}