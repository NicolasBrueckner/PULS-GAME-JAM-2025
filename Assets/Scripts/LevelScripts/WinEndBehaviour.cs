#region

using UnityEngine;
using static Kaputt;

#endregion

public class WinEndBehaviour : MonoBehaviour
{
    public AudioClip winSound;
    public AudioSource audioSource;

    private LayerMask _playerLayer;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
	}

	private void Awake()
	{
		_playerLayer = 1 << LayerMask.NameToLayer( "Player" );
	}

	private void OnTriggerEnter( Collider other )
	{
		if( !IsInLayerMask( other.gameObject, _playerLayer ) )
			return;

        audioSource.clip = winSound;
        audioSource.Play();
        Debug.Log( "player should win" );

		_gem.OnGameWon();
	}
}