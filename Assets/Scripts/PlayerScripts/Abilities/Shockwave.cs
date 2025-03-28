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
	public GameObject shockwaveEffect;
	public LayerMask affectedLayers;

    public AudioClip shockwaveSound;
    public AudioClip destroySound;
    public AudioSource audioSourceAbility;
    public AudioSource audioSourceDestroy;

    private bool _isDestroying;

	private Coroutine _shockwaveCooldownCoroutine;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.ShockwavePerformed += OnShockwavePerformedReceived;
	}

	private void OnDisable()
	{
		_isDestroying = false;
		shockwaveEffect.SetActive( false );
		_shockwaveCooldownCoroutine = null;
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

        audioSourceDestroy.clip = destroySound;
        audioSourceDestroy.Play();
    }

	private void OnShockwavePerformedReceived()
	{
		if( !IsActive )
			return;

        audioSourceAbility.clip = shockwaveSound;
        audioSourceAbility.Play();

        _isDestroying = true;
		_shockwaveCooldownCoroutine ??= StartCoroutine( ShockwaveCooldown() );
	}

	private IEnumerator ShockwaveCooldown()
	{
		shockwaveEffect.SetActive( true );

		yield return new WaitForFixedUpdate();

		float timer = cooldown;

		while( ( timer -= Time.fixedDeltaTime ) > 0 )
		{
			if( timer <= cooldown / 3 )
			{
				_isDestroying = false;
				shockwaveEffect.SetActive( false );
			}

			yield return new WaitForFixedUpdate();
		}

		_shockwaveCooldownCoroutine = null;
	}


	public void ChangeActivityStatus( bool isActive )
	{
		enabled = isActive;
		gameObject.SetActive( isActive );
	}
}