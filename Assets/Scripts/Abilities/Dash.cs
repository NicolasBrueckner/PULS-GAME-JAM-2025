#region

using System.Collections;
using UnityEngine;

#endregion

public class Dash : MonoBehaviour, IAbility
{
	public float dashFactor;
	public float dashDuration;
	public float dashCooldown;

	private Coroutine _timedDashCoroutine;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.BashDashPerformed += OnBashDashPerformedReceived;
	}

	private void OnBashDashPerformedReceived()
	{
		if( enabled )
			_timedDashCoroutine ??= StartCoroutine( TimedDash() );
	}

	private IEnumerator TimedDash()
	{
		_gem.OnMoveSpeedChange( dashFactor );

		float timer = dashDuration;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();

		_gem.OnMoveSpeedChange( 1f );

		timer = dashCooldown;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();
	}

	public void ChangeActivityStatus( bool isActive )
	{
		enabled = isActive;
	}
}