#region

using System.Collections;
using UnityEngine;

#endregion

public class Bash : MonoBehaviour, IAbility
{
	public float bashFactor;
	public float bashDuration;
	public float bashCooldown;
	public LayerMask affectedLayers;

	private Coroutine _timedBashCoroutine;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.BashDashPerformed += OnBashDashPerformedReceived;
	}

	private void OnBashDashPerformedReceived()
	{
		if(enabled)
		_timedBashCoroutine ??= StartCoroutine( TimedBash() );
	}

	private IEnumerator TimedBash()
	{
		_gem.OnMoveSpeedChange( bashFactor );

		float timer = bashDuration;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();

		_gem.OnMoveSpeedChange( 1f );

		timer = bashCooldown;
		while( ( timer -= Time.fixedDeltaTime ) > 0f )
			yield return new WaitForFixedUpdate();
	}

	public void ChangeActivityStatus( bool isActive )
	{
		enabled = isActive;
	}
}