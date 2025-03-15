#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion


//only for movement an abilities
[ RequireComponent( typeof( Rigidbody ) ) ]
public class MovementBehaviour : MonoBehaviour
{
	public float moveSpeed;
	public GameObject movementBounds;

	private Rigidbody _rb3d;
	private Vector3 _bufferedMovement;
	private float _currentMoveSpeed;

	private PlayerInputEventManager _piem => PlayerInputEventManager.Instance;
	private GameplayEventManager _gem => GameplayEventManager.Instance;

	private void Awake()
	{
		_rb3d = GetComponent<Rigidbody>();
		_currentMoveSpeed = moveSpeed;
	}

	private void Start()
	{
		_piem.MovePerformed += OnMovePerformedReceived;
		_piem.MoveCanceled += OnMoveCanceledReceived;
		_gem.MoveSpeedChange += UpdateCurrentMoveSpeed;
	}

	private void FixedUpdate()
	{
		if( movementBounds != null )
		{
			Vector3 newPosition = _rb3d.position + ( Vector3 )_bufferedMovement * Time.fixedDeltaTime;
			if( IsWithinBounds( newPosition ) )
				_rb3d.linearVelocity = _bufferedMovement;
			else
				_rb3d.linearVelocity = Vector2.zero;
		}
		else
		{
			_rb3d.linearVelocity = _bufferedMovement;
		}
	}

	private void OnMovePerformedReceived( InputAction.CallbackContext ctx )
	{
		UpdateBufferedMovement( ctx.ReadValue<Vector2>() );
	}

	private void OnMoveCanceledReceived( InputAction.CallbackContext ctx )
	{
		UpdateBufferedMovement( ctx.ReadValue<Vector2>() );
	}

	private void UpdateCurrentMoveSpeed( float factor )
	{
		_currentMoveSpeed = moveSpeed * factor;
	}

	private void UpdateBufferedMovement( Vector2 input )
	{
		_bufferedMovement = input * _currentMoveSpeed;
	}

	private bool IsWithinBounds( Vector3 targetPosition )
	{
		if( movementBounds == null ) return true;

		Bounds bounds = movementBounds.GetComponent<Collider>().bounds;
		return bounds.Contains( targetPosition );
	}
}