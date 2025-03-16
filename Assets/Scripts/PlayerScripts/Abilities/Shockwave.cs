#region

using System.Collections;
using UnityEngine;
using static Kaputt;

#endregion

[ RequireComponent( typeof( Collider ) ) ]
public class Shockwave : MonoBehaviour, IAbility
{
	public bool IsActive => enabled;

	public float cooldown;
	public LayerMask affectedLayers;

	private bool _isDestroying;

	private Coroutine _shockwaveCooldownCoroutine;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.ShockwavePerformed += OnShockwavePerformedReceived;
	}

	private void OnDestroy()
	{
		_piem.ShockwavePerformed -= OnShockwavePerformedReceived;
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
		_gem.OnPlayerHeal();
		_isDestroying = true;
		_shockwaveCooldownCoroutine ??= StartCoroutine( ShockwaveCooldown() );
	}

	private IEnumerator ShockwaveCooldown()
	{
		yield return new WaitForFixedUpdate();

		_isDestroying = false;

		float timer = cooldown;

		while( ( timer -= Time.fixedDeltaTime ) > 0 )
			yield return new WaitForFixedUpdate();

		_shockwaveCooldownCoroutine = null;
	}


	public void ChangeActivityStatus( bool isActive )
	{
		Debug.Log( $"ability {this} set to {isActive}" );
		enabled = isActive;
	}
}