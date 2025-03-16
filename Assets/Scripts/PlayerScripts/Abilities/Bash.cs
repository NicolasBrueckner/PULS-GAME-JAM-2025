#region

using System.Collections;
using UnityEngine;
using static Kaputt;

#endregion

[ RequireComponent( typeof( Collider ) ) ]
public class Bash : MonoBehaviour, IAbility
{
	public bool IsActive => enabled;

	public float bashDuration;
	public float bashCooldown;
	public GameObject bashEffect;
	public LayerMask affectedLayers;

	private bool _isBashing;
	private Coroutine _timedBashCoroutine;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.BashDashPerformed += OnBashDashPerformedReceived;
	}

	private void OnDisable()
	{
		bashEffect.SetActive( false );
		_timedBashCoroutine = null;
	}

	private void OnDestroy()
	{
		_piem.BashDashPerformed -= OnBashDashPerformedReceived;
	}

	private void OnTriggerStay( Collider other )
	{
		if( !_isBashing )
			return;

		if( !IsInLayerMask( other.gameObject, affectedLayers ) )
			return;

		IDestroyable d = other.GetComponent<IDestroyable>();
		d?.DestroyInterfaceMember();
	}

	private void OnBashDashPerformedReceived()
	{
		if( !IsActive )
			return;

		_timedBashCoroutine ??= StartCoroutine( TimedBash() );
	}

	private IEnumerator TimedBash()
	{
		_gem.OnPlayerInvincible( true );
		_isBashing = true;
		bashEffect.SetActive( true );

		float timer = bashDuration;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();

		_gem.OnPlayerInvincible( false );
		_isBashing = false;
		bashEffect.SetActive( false );

		timer = bashCooldown;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();

		_timedBashCoroutine = null;
	}

	public void ChangeActivityStatus( bool isActive )
	{
		enabled = isActive;
		gameObject.SetActive( isActive );
	}
}