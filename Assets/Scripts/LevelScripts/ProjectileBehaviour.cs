#region

using UnityEngine;
using Random = System.Random;
using static Kaputt;

#endregion

[ RequireComponent( typeof( Rigidbody ) ) ]
public class ProjectileBehaviour : MonoBehaviour, IFixedUpdateObserver
{
	public LayerMask playerLayer;
	public float minSpeed;
	public float maxSpeed;
	public float minAngle;
	public float maxAngle;
	public float outOfRangeSqrDistance;

	private readonly Random _rng = new();
	private Rigidbody _rb;

	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void OnEnable()  => FixedUpdateEventManager.RegisterObserver( this );
	private void OnDisable() => FixedUpdateEventManager.UnregisterObserver( this );

	public void ObservedFixedUpdate()
	{
		if( IsOutOfRange() )
			DeactivateProjectile();
	}

	private void OnTriggerEnter( Collider other )
	{
		if( !IsInLayerMask( other.gameObject, playerLayer ) )
			return;

		GameplayEventManager.Instance?.OnPlayerCriticalHit();
		DeactivateProjectile();
	}

	public void StartFlying()
	{
		Vector3 direction = GenerateRandomDirection();
		float rngSpeed = minSpeed + ( float )( ( maxSpeed - minSpeed ) * _rng.NextDouble() );

		_rb.AddForce( direction * rngSpeed, ForceMode.Impulse );
	}

	private bool IsOutOfRange() => transform.position.sqrMagnitude > outOfRangeSqrDistance;

	private Vector3 GenerateRandomDirection()
	{
		float angle = ( float )( _rng.NextDouble() * ( maxAngle - minAngle ) + minAngle );
		Quaternion rotation = Quaternion.Euler( 0, angle, 0 );

		return rotation * Vector3.forward;
	}

	private void DeactivateProjectile()
	{
		_rb.linearVelocity = Vector3.zero;
		_gem.OnProjectileDeactivate( gameObject );
	}
}