#region

using UnityEngine;

#endregion

//[RequireComponent(typeof(SphereCollider))]
public class Shockwave : MonoBehaviour, IAbility
{
	public float range;
	public LayerMask affectedLayers;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Start()
	{
		_piem.ShockwavePerformed += OnShockwavePerformedReceived;
	}

	private void OnShockwavePerformedReceived()
	{
		Collider[] hits = Physics.OverlapSphere( transform.position, range );

		foreach( Collider hit in hits )
		{
		}
	}

	public void ChangeActivityStatus( bool isActive )
	{
		enabled = isActive;
	}
}