#region

using System.Collections;
using UnityEngine;
using static Kaputt;

#endregion

[ RequireComponent( typeof( Collider ) ) ]
public class Shockwave : MonoBehaviour, IAbility
{
	public float cooldown;
	public LayerMask affectedLayers;

	private bool _isDestroying;
	private bool _isDrawing;

	private Coroutine _shockwaveCooldownCoroutine;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.ShockwavePerformed += OnShockwavePerformedReceived;
	}

	private void OnTriggerStay( Collider other )
	{
		if( !_isDestroying )
			return;

		if( !IsInLayerMask( other.gameObject, affectedLayers ) )
			return;

		IDestroyable d = other.GetComponent<IDestroyable>();
		d?.DestroyInterfaceMember();
	}

	private void OnShockwavePerformedReceived()
	{
		_isDestroying = true;
		_isDrawing = true;
		_shockwaveCooldownCoroutine ??= StartCoroutine( ShockwaveCooldown() );
	}

	private IEnumerator ShockwaveCooldown()
	{
		yield return new WaitForFixedUpdate();

		_isDestroying = false;

		float timer = cooldown;

		while( ( timer -= Time.fixedDeltaTime ) > 0 )
			yield return new WaitForFixedUpdate();

		_isDrawing = false;
		_shockwaveCooldownCoroutine = null;
	}

	public void ChangeActivityStatus( bool isActive )
	{
		enabled = isActive;
	}
}