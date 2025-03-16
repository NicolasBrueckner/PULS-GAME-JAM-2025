#region

using System.Collections;
using UnityEngine;

#endregion

public class Dash : MonoBehaviour, IAbility
{
	public bool IsActive => enabled;

	public float dashFactor;
	public float dashDuration;
	public float dashCooldown;
	public GameObject dashObject;
	public GameObject dashEffect;

	private Coroutine _timedDashCoroutine;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.BashDashPerformed += OnBashDashPerformedReceived;
	}

	private void OnDestroy()
	{
		_piem.BashDashPerformed -= OnBashDashPerformedReceived;
	}

	private void OnBashDashPerformedReceived()
	{
		if( !IsActive )
			return;

		_timedDashCoroutine ??= StartCoroutine( TimedDash() );
	}

	private IEnumerator TimedDash()
	{
		_gem.OnMoveSpeedChange( dashFactor );
		dashEffect.SetActive( true );

		float timer = dashDuration;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();

		_gem.OnMoveSpeedChange( 1f );
		dashEffect.SetActive( false );

		timer = dashCooldown;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();

		_timedDashCoroutine = null;
	}

	public void ChangeActivityStatus( bool isActive )
	{
		enabled = isActive;
		dashObject.SetActive( isActive );
		gameObject.SetActive( isActive );
	}
}